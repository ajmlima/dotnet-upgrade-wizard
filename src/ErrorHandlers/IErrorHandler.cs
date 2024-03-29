using System.CommandLine.Invocation;

namespace WizardTool.ErrorHandlers
{
    public interface IErrorHandler
    {
        Task HandleErrors(InvocationContext context, Func<InvocationContext, Task> next);
    }
}