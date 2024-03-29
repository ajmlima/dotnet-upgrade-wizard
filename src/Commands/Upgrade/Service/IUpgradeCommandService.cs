namespace WizardTool.Commands.Upgrade.Service
{    
    internal interface IUpgradeCommandService
    {       
        Task HandleAsync(UpgradeParameters parameters);
    }
}