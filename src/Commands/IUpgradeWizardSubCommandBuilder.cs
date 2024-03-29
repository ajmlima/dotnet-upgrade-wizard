using System.CommandLine;

namespace WizardTool.Commands
{
    public interface IUpgradeWizardSubCommandBuilder
    {
        Command Build();
    }
}