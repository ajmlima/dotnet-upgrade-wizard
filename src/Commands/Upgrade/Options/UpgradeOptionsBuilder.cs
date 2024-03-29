using System.CommandLine;

namespace WizardTool.Commands.Upgrade.Options
{    
    internal static class UpgradeOptionsBuilder
    {
        private static readonly string[] TargetFrameworkAliases = { "--targetFramework", "-t" };
        private static readonly string[] SolutionNameAliases = { "--solutionName", "-s" };
        private static readonly string[] VerboseOutputAliases = { "--verboseOutput", "-vo" };

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
        
        public static Option<bool> BuildVerboseOutputOption()
        {
            return new Option<bool>(VerboseOutputAliases, getDefaultValue:() => false, "Prints the upgrade-assistant tool logs")
            {
                IsRequired = false
            };
        }  
    }
}