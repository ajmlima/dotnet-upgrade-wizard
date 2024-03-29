using WizardTool.Services.Interfaces;

namespace WizardTool.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void Write(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteInfo(string value)
        {
            WriteLine("Info > " + value);
        }

        public void WriteInput(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("Ok > " + value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteSample(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLine("Example > " + value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine("Error > " + value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void WriteLine(string value)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }
    }
}