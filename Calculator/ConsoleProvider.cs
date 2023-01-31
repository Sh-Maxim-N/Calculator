using Calculator.Interfaces;
using Calculator.Services;

namespace Calculator;

public class ConsoleProvider : CalculatorManager
{
    private IConsoleIo _consoleIo;
    private IValidator _validator;
    private string userInput;
    private string result;

    public ConsoleProvider(IValidator validator, IConsoleIo consoleIo)
    {
        this. _consoleIo = consoleIo ?? throw new ArgumentException(nameof(consoleIo));
        this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    protected override void ReadFileOrExpression()
    {
        for (;;)
        {
            _consoleIo.WriteLine(Messages.EnterExpression);
            var checkUserInput = _consoleIo.ReadLine();

            if (!_validator.CheckIfConsoleExpressionValid(checkUserInput))
            {
                _consoleIo.WriteLine(Messages.InputError);
                checkUserInput = _consoleIo.ReadLine();
            }

            userInput = checkUserInput;



            ProcessExpression();

            _consoleIo.WriteLine(result);
            if (AnotherExpression())
            {
                continue;
            }
            break;
        }
    }


    private bool AnotherExpression()
    {
        _consoleIo.WriteLine(Messages.RepeatCalculation);

        var anotherExpression = _consoleIo.ReadLine();
        if (int.TryParse(anotherExpression, out int d) && d == 1)
        {
            return true;
        }
        return false;
    }

    protected override void ProcessExpression()
    {
        result = CalculationWithMathPriority(userInput);
    }
}