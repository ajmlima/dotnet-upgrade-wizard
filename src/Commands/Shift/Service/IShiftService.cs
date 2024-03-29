namespace WizardTool.Commands.Shift.Service
{    
    internal interface IShiftService
    {       
        Task HandleAsync(ShiftParameters parameters);
    }
}