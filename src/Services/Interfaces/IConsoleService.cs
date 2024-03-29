namespace WizardTool.Services.Interfaces
{
    public interface IConsoleService
    {
        string? ReadLine();

        void WriteInput(string value);
        void WriteSample(string value);
        void WriteError(string value);
        void WriteSuccess(string value);
        void WriteInfo(string value);
        void WriteLine();
        void Write(string value);
    }
}