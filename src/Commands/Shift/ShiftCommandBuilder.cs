using System.CommandLine;
using WizardTool.Commands.Shift.Options;
using WizardTool.Commands.Shift.Service;

namespace WizardTool.Commands.Shift
{                    
    internal class ShiftCommandBuilder : IUpgradeWizardSubCommandBuilder
    {
        private const string CommandName = "shift";

        private const string CommandDescription =
            "Detects all the projects in the solution and shifts the .NET version to the provided target version";
        private readonly IShiftService _shiftService;

        public ShiftCommandBuilder(IShiftService shiftService)
        {                    
            _shiftService = shiftService;
        }

        public Command Build()
        {
            var targetFrameworkOption = ShiftOptionsBuilder.BuildTargetFrameworkOption();
            var solutionNameOption = ShiftOptionsBuilder.BuildSolutionNameOption();
            var command = new Command(CommandName, CommandDescription)
            {
                targetFrameworkOption,
                solutionNameOption
            };

            command.SetHandler(async (targetFrameworkValue, solutionNameValue) =>
            {
                
                await _shiftService.HandleAsync(new ShiftParameters(targetFrameworkValue, solutionNameValue));
            },targetFrameworkOption, solutionNameOption);
            
            return command;
        }
    }
}