namespace Calculator.Interfaces;

public interface IConsoleIo
{
    void WriteLine(object message);
    string ReadLine();
}