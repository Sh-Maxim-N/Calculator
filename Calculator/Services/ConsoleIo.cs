using Calculator.Interfaces;

namespace Calculator.Services;

public class ConsoleIo : IConsoleIo
{
    public void WriteLine(object message)
    {
        Console.WriteLine(message.ToString());
    }
    public string ReadLine()
    {
        return Console.ReadLine();
    }

}