using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using WizardTool.Commands;
using WizardTool.ErrorHandlers;

namespace WizardTool.App
{
    public class App
    {
        public App(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private IServiceProvider ServiceProvider { get; }

        public Task<int> RunAsync(string[] args)
        {
            var rootCommand = ServiceProvider.GetService<IUpgradeWizardCommandBuilder>()?.Build();
            var errorHandler = ServiceProvider.GetRequiredService<IErrorHandler>();

            var commandLineBuilder = new CommandLineBuilder(rootCommand);
            
            commandLineBuilder.AddMiddleware(errorHandler.HandleErrors);
            commandLineBuilder.UseDefaults();

            var parser = commandLineBuilder.Build();

            var option = parser.Configuration.RootCommand.Options.Single(o => o.Name == "version");
            option?.AddAlias("-v");

            return parser.InvokeAsync(args);
        }
    }
}