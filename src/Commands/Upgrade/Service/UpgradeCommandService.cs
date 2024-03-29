using WizardTool.Services.Interfaces;

namespace WizardTool.Commands.Upgrade.Service
{    
    internal class UpgradeCommandService : IUpgradeCommandService
    {
        private readonly IConsoleService _consoleService;
        private readonly ISolutionService _solutionService;
        private readonly IDotNetUpgradeService _dotNetUpgradeService;

        public UpgradeCommandService(IConsoleService consoleService, ISolutionService solutionService, IDotNetUpgradeService dotNetUpgradeService)
        {
            _consoleService = consoleService;
            _solutionService = solutionService;
            _dotNetUpgradeService = dotNetUpgradeService;
        }
        
        public async Task HandleAsync(UpgradeParameters parameters)
        {
            var atLeastOneProjectFailedUpgrade = false;
            var solutionInfo = _solutionService.GetSolution(parameters.SolutionName);

            if (solutionInfo?.Projects is null || solutionInfo.Projects.Length == 0)
            {
                _consoleService.WriteInfo("There are no projects to upgrade.");
                return;
            }

            foreach (var projectPath in solutionInfo.Projects)
            {
                var projectName = Path.GetFileName(projectPath);
        
                _consoleService.WriteInfo($"Starting the upgrade of {projectName} project to version {parameters.TargetFramework} ...");

                var upgradeResult = _dotNetUpgradeService.UpgradeProject(projectPath, parameters.TargetFramework, parameters.VerboseOutputEnabled);

                if (upgradeResult.Success)
                {
                    _consoleService.WriteSuccess($"The {projectName} was successfully upgraded to version {parameters.TargetFramework}.");
                }
                else
                {
                    _consoleService.WriteError($"The {projectName} failed to upgrade to version {parameters.TargetFramework}. {upgradeResult.NumberFailed} upgrade tasks have failed.");
                    _consoleService.WriteError("Please run the tool with the --verboseOutput option to help identify and validate the problems.");
                    atLeastOneProjectFailedUpgrade = true;
                }
            }
            
            if (atLeastOneProjectFailedUpgrade)
            {
                _consoleService.WriteInfo("At least one project failed to upgrade. Please run the tool with the --verboseOutput option to help identify and validate the problems.");
            }
            else
            {
                var updateGlobalJsonSdkResult = await _dotNetUpgradeService.UpdateGlobalJsonSdkAsync(solutionInfo.Path, parameters.TargetFramework);

                if (updateGlobalJsonSdkResult.Success)
                {
                    _consoleService.WriteSuccess("The global.json file was successfully updated.");
                }
            }
            
            _consoleService.WriteInfo("The upgrade process has finished!");
            _consoleService.WriteInfo("Please remember to manually check and test that all the changes made are indeed what you expected.");
        }
    }
}