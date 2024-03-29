using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WizardTool.App;
using WizardTool.Commands;
using WizardTool.Commands.Upgrade;
using WizardTool.Commands.Upgrade.Service;
using WizardTool.ErrorHandlers;
using WizardTool.Services;
using WizardTool.Services.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IErrorHandler, ErrorHandler>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IUpgradeWizardCommandBuilder, UpgradeWizardCommandBuilder>();
        
        services.AddSingleton<IUpgradeWizardSubCommandBuilder, UpgradeCommandBuilder>();
        services.AddSingleton<IUpgradeCommandService, UpgradeCommandService>();
        services.AddSingleton<ISolutionService, SolutionService>();
        services.AddSingleton<IDotNetUpgradeService, DotNetUpgradeService>();

    })
    .Build();

var serviceProvider = host.Services;

await new App(serviceProvider).RunAsync(args);
