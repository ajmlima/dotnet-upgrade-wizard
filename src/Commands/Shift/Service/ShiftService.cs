using WizardTool.Services;
using WizardTool.Services.Interfaces;

namespace WizardTool.Commands.Shift.Service
{    
    internal class ShiftService : IShiftService
    {
        private readonly IConsoleService _consoleService;

        public ShiftService(IConsoleService consoleService)
        {
            _consoleService = consoleService;
        }
        
        public async Task HandleAsync(ShiftParameters parameters)
        {
            if (parameters.UpgradeSpecificSolution())
            {
                var specifiedSolutionPath = Directory.GetFiles(Directory.GetCurrentDirectory(), parameters.SolutionName, SearchOption.AllDirectories)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(specifiedSolutionPath))
                {
                    _consoleService.WriteError($"Error: The specified solution file '{parameters.SolutionName}' was not found in the current directory.");
                    return;
                }
                
                _consoleService.WriteSuccess("Ok!");
            }
            else
            {
                var allSolutionFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.sln", SearchOption.AllDirectories);

                if (allSolutionFiles.Length > 1)
                {
                    _consoleService.WriteError($"Error: More than one solution was found in the current directory. Specify the solution name you want to upgrade.");
                    return;
                }
                /*//var specifiedSolutionPath = allSolutionFiles.FirstOrDefault(file => Path.GetFileName(file).Equals(solutionName, StringComparison.OrdinalIgnoreCase));

                if (specifiedSolutionPath != null)
                {
                    // A solution file matching the specified name was found.
                }
                else
                {
                    // No matching solution file was found.
                }*/
            }

            return;
        }
    }
}