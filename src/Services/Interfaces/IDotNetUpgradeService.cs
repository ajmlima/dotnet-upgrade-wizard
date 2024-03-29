using WizardTool.Services.Models;

namespace WizardTool.Services.Interfaces;

public interface IDotNetUpgradeService
{
    UpgradeResult UpgradeProject(string projectPath, string targetFramework, bool verboseOutputEnabled);
    Task<UpgradeResult> UpdateGlobalJsonSdkAsync(string solutionPath, string targetFramework);
}