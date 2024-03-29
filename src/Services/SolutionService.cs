using WizardTool.Services.Interfaces;
using WizardTool.Services.Models;

namespace WizardTool.Services;

public class SolutionService : ISolutionService
{
    private readonly IConsoleService _consoleService;

    public SolutionService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
    
    public DotnetSolution? GetSolution(string solutionName)
    {
        string solutionPath;
        
        if (!string.IsNullOrEmpty(solutionName))
        {
            var specifiedSolutionPath = Directory.GetFiles(Directory.GetCurrentDirectory(), solutionName, SearchOption.AllDirectories)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(specifiedSolutionPath))
            {
                _consoleService.WriteError($"The solution '{solutionName}' was not found in the current directory. Are you sure it resides here?");
                return null;
            }

            solutionPath = specifiedSolutionPath;
        }
        else
        {
            var allSolutionFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sln", SearchOption.AllDirectories);

            if (allSolutionFiles.Length == 0)
            {
                _consoleService.WriteError("No solutions have been found under this directory. We need to look elsewhere or bring one here.");
                return null;
            }
            
            if (allSolutionFiles.Length > 1)
            {
                _consoleService.WriteError("Multiple solutions have been found under this directory. To choose wisely, tell me the name of the solution you seek, using the --solutionName option.");
                return null;
            }

            solutionPath = allSolutionFiles.First();
        }

        var solutionDirectory = Path.GetDirectoryName(solutionPath);
        var name = Path.GetFileName(solutionPath);
        
        if (string.IsNullOrEmpty(solutionDirectory))
        {
            _consoleService.WriteError("Something unexpected happened. I was unable to retrieve the solution directory.");
            return null;
        }
        
        var solutionProjects = Directory.GetFiles(solutionDirectory, "*.csproj", SearchOption.AllDirectories);

        return new DotnetSolution(name, solutionDirectory, solutionProjects);
    }
}