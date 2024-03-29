using System.CommandLine;

namespace WizardTool.Commands.Shift.Options
{    
    internal static class ShiftOptionsBuilder
    {
        private static readonly string[] TargetFrameworkAliases = { "--targetFramework", "-t" };
        private static readonly string[] SolutionNameAliases = { "--solutionName", "-s" };

        public static Option<string> BuildTargetFrameworkOption()
        {
            return new Option<string>(TargetFrameworkAliases, "Specify the target .NET version for the solution upgrade. Supported versions: net8.0")
            {
                IsRequired = true
            };
        }
        public static Option<string> BuildSolutionNameOption()
        {
            return new Option<string>(SolutionNameAliases, "Specify the solution file to upgrade when multiple solutions are detected")
            {
                IsRequired = false
            };
        }                                       
    }
}