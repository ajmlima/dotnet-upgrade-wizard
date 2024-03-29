using System.CommandLine;

namespace WizardTool.Commands
{
    public class UpgradeWizardCommandBuilder : IUpgradeWizardCommandBuilder
    {
        private const string CommandName = "upgrade-wizard";
        private readonly IEnumerable<IUpgradeWizardSubCommandBuilder> _upgradeWizardSubCommandBuilders;

        public UpgradeWizardCommandBuilder(IEnumerable<IUpgradeWizardSubCommandBuilder> upgradeWizardSubCommandBuilders)
        {
            _upgradeWizardSubCommandBuilders = upgradeWizardSubCommandBuilders;
        }

        public RootCommand Build()
        {
            var rootCommand = new RootCommand
            {
                Name = CommandName,
                Description = @"Run 'upgrade-wizard [command] --help' in order to get specific information."
            };

            _upgradeWizardSubCommandBuilders.ToList().ForEach(builder => rootCommand.AddCommand(builder.Build()));
            return rootCommand;
        }
    }
}