namespace WizardTool.Commands.Shift
{    
    internal class ShiftParameters
    {
        public ShiftParameters(string targetFramework, string solutionName)
        {
            TargetFramework = targetFramework;
            SolutionName = solutionName;
        }

        public string TargetFramework { get; }
        public string SolutionName { get; }

        public bool UpgradeSpecificSolution()
        {
            return !string.IsNullOrEmpty(SolutionName);
        }
    }
}