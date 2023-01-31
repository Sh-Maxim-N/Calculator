namespace Calculator.Interfaces;

public interface IValidator
{
    bool CheckIfExpressionsInFileValid(string input);
    bool CheckIfConsoleExpressionValid(string? input); 
}