namespace WizardTool.Commands.Upgrade
{    
    internal class UpgradeParameters
    {
        public UpgradeParameters(string targetFramework, string solutionName, bool verboseOutputEnabled)
        {
            TargetFramework = targetFramework;
            SolutionName = solutionName;
            VerboseOutputEnabled = verboseOutputEnabled;
        }

        public string TargetFramework { get; }
        public string SolutionName { get; }
        
        public bool VerboseOutputEnabled { get; }
    }
}