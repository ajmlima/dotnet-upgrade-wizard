using WizardTool.Services.Models;

namespace WizardTool.Services.Interfaces;

public interface ISolutionService
{
    DotnetSolution? GetSolution(string solutionName);
}