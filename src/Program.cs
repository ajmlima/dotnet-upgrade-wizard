using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WizardTool.App;
using WizardTool.Commands;
using WizardTool.Commands.Shift;
using WizardTool.Commands.Shift.Service;
using WizardTool.ErrorHandlers;
using WizardTool.Services;
using WizardTool.Services.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddSingleton<IErrorHandler, ErrorHandler>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IUpgradeWizardCommandBuilder, UpgradeWizardCommandBuilder>();
        
        services.AddSingleton<IUpgradeWizardSubCommandBuilder, ShiftCommandBuilder>();
        services.AddSingleton<IShiftService, ShiftService>();

    })
    .Build();

var serviceProvider = host.Services;

await new App(serviceProvider).RunAsync(args);
