using System.CommandLine;
using WizardTool.Commands.Upgrade.Options;
using WizardTool.Commands.Upgrade.Service;

namespace WizardTool.Commands.Upgrade
{                    
    internal class UpgradeCommandBuilder : IUpgradeWizardSubCommandBuilder
    {
        private const string CommandName = "upgrade";

        private const string CommandDescription =
            "Detects all the projects in the solution and upgrades the .NET version to the provided target version";
        private readonly IUpgradeCommandService _upgradeCommandService;

        public UpgradeCommandBuilder(IUpgradeCommandService upgradeCommandService)
        {                    
            _upgradeCommandService = upgradeCommandService;
        }

        public Command Build()
        {
            var targetFrameworkOption = UpgradeOptionsBuilder.BuildTargetFrameworkOption();
            var solutionNameOption = UpgradeOptionsBuilder.BuildSolutionNameOption();
            var verboseOutputOption = UpgradeOptionsBuilder.BuildVerboseOutputOption();
            var command = new Command(CommandName, CommandDescription)
            {
                targetFrameworkOption,
                solutionNameOption,
                verboseOutputOption
            };

            command.SetHandler(async (targetFrameworkValue, solutionNameValue, verboseOutputValue) =>
            {
                
                await _upgradeCommandService.HandleAsync(new UpgradeParameters(targetFrameworkValue, solutionNameValue, verboseOutputValue));
            },targetFrameworkOption, solutionNameOption, verboseOutputOption);
            
            return command;
        }
    }
}