using System.CommandLine;

namespace WizardTool.Commands
{
    public interface IUpgradeWizardCommandBuilder
    {
        RootCommand Build();
    }
}