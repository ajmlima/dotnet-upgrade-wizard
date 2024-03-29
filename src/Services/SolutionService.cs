using WizardTool.Services.Interfaces;

namespace WizardTool.Services;

public class SolutionService : ISolutionService
{
    private readonly IConsoleService _consoleService;

    public SolutionService(IConsoleService consoleService)
    {
        _consoleService = consoleService;
    }
    
    public string[] GetSolutionProjects(string solutionName)
    {
        throw new NotImplementedException();
    }
}