using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WizardTool.ErrorHandlers;
using WizardTool.Services.Interfaces;
using WizardTool.Services.Models;

namespace WizardTool.Services;

public partial class DotNetUpgradeService: IDotNetUpgradeService
{
    private const int UpgradeSentenceLoopFrequency = 15;
    private readonly IConsoleService _consoleService;

    public DotNetUpgradeService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }

    public UpgradeResult UpgradeProject(string projectPath, string targetFramework, bool verboseOutputEnabled)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "upgrade-assistant",
                Arguments = $"upgrade \"{projectPath}\" --targetFramework {targetFramework} --non-interactive",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        
        process.Start();
        var output = string.Empty;
        var loopCount = 0;

        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            if (verboseOutputEnabled && !string.IsNullOrEmpty(line))
            {
                _consoleService.WriteInfo(line);
            }else if (loopCount % UpgradeSentenceLoopFrequency == 0) 
            {
                _consoleService.Write("-");
            }

            output += line;
            loopCount++;
        }

        process.WaitForExit();

        // Regex to find the summary line
        var resultRegex = UpgradeAssistantResultRegex();
        var match = resultRegex.Match(output);

        if (match.Success)
        {
            var numberSucceeded = int.Parse(match.Groups[1].Value);
            var numberFailed = int.Parse(match.Groups[2].Value);
            var numberSkipped = int.Parse(match.Groups[3].Value);
            var succeeded = numberFailed == 0;

            return new UpgradeResult(succeeded, numberSucceeded, numberFailed, numberSkipped);
        }

        // If we can't find a summary, assume failure to be cautious
        return new UpgradeResult(false);
    }

    public async Task<UpgradeResult> UpdateGlobalJsonSdkAsync(string solutionPath, string targetFramework)
    {
        try
        {
            _consoleService.WriteInfo("Updating global.json file...");
            
            var globalJsonFile = Directory.GetFiles(solutionPath, "global.json", SearchOption.AllDirectories).FirstOrDefault();

            if (string.IsNullOrEmpty(globalJsonFile))
            {
                _consoleService.WriteError("The global.json file was not found.");
                return new UpgradeResult(false);
            }
            
            var jsonContent = await File.ReadAllTextAsync(globalJsonFile);
            
            if (string.IsNullOrEmpty(jsonContent))
            {
                _consoleService.WriteError("The global.json file was found but it was not possible to parse it.");
                return new UpgradeResult(false);
            }

            dynamic globalJson = JsonConvert.DeserializeObject(jsonContent)!;
            
            // Update the SDK version and other properties as needed
            globalJson.sdk.version = targetFramework switch
            {
                "net8.0" => "8.0.0",
                _ => globalJson.sdk.version
            };

            // Convert the updated object back to a JSON string
            string updatedJsonContent = JsonConvert.SerializeObject(globalJson, Formatting.Indented);

            // Write the updated JSON string back to the global.json file
            await File.WriteAllTextAsync(globalJsonFile, updatedJsonContent);

            return new UpgradeResult(true);
        }
        catch (Exception ex)
        {
            _consoleService.WriteError("Something went wrong when updating the global.json file. See the exception:");
            throw new UpgradeWizardException(ex.ToString());
        }
    }

    [GeneratedRegex(@"Complete: (\d+) succeeded, (\d+) failed, (\d+) skipped.")]
    private static partial Regex UpgradeAssistantResultRegex();
}