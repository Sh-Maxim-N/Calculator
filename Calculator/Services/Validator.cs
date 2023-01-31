using Calculator.Interfaces;
using System.Text.RegularExpressions;

namespace Calculator.Services;

public class Validator : IValidator
{

    public bool CheckIfExpressionsInFileValid(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        if (!ValidChecker(input, @"[0-9]|\+|\-|\*|\/|\(|\)|[\d,\d]"))
        {
            return false;
        }

        if (ValidChecker(input, @"[\/\*\+\-][\/\*\+]"))
        {
            return false;
        }

        if (ValidChecker(input, @"[A-Za-zА-Яа-я\:;\$<=>\&\?@\{\}\[\]|_^`!\'=]|\(\)"))
        {
            return false;
        }

        if (!input.Contains('('))
        {
            return true;
        }

        if (!AreAllBracketsClosed(input))
        {
            return false;
        }

        return true;
    }


    public bool CheckIfConsoleExpressionValid(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        if (ValidChecker(input, @"[\/\*\+\-\(][\/\*\+\)]"))
        {
            return false;
        }


        if (ValidChecker(input, @"[A-Za-zА-Яа-я\(\)\:;\$<=>\&\?@\{\}\[\]|_^`!\'=]"))
        {
            return false;
        }

        return true;
    }


    private bool AreAllBracketsClosed(string? input)
    {
        return input.Count(c => c == '(') == input.Count(c => c == ')');
    }

    private bool ValidChecker(string inputString, string regex)
    {
        Regex patternWithBraces = new Regex(regex);

        if (patternWithBraces.IsMatch(inputString))
        {
            return true;
        }
        return false;
    }
}