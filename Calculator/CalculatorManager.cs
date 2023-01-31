using Calculator.Interfaces;
using System.Text.RegularExpressions;
using Calculator.Services;

namespace Calculator;

public abstract class CalculatorManager
{
    public void TemplateMethod()
    {
        ReadFileOrExpression();
        ProcessExpression();
    }

    protected abstract void ReadFileOrExpression(); 
    protected abstract void ProcessExpression();

    protected string CalculationWithMathPriority(string input)
    {
        string res = input;
        res = MathOperations(res, @"(((?<=(^|[\*\/\+\-]))\-)?\d+(,\d+)*)([\*\/])(\-?\d+(,\d+)*)");
        res = MathOperations(res, @"(((?<=(^|[\*\/\+\-]))\-)?\d+(,\d+)*)([\+\-])(\-?\d+(,\d+)*)");

        return res;
    }

    private string MathOperations(string singleMathExpression, string regexp)
    {
        bool bReplace;
        string strOut = singleMathExpression;

        do
        {
            bReplace = false;
            int ReplaceCount = 0;
            strOut = Regex.Replace(strOut, regexp, match =>
            {
                if (ReplaceCount > 0)
                    return match.Value;
                ReplaceCount++;

                double a = double.Parse(match.Groups[1].Value);
                double b = double.Parse(match.Groups[6].Value);

                double res = 0;

                if (match.Groups[5].Value == "*")
                    res = a * b;

                if (b == 0 & match.Groups[5].Value == "/")
                {
                    throw new ArgumentException("Can Not Divide By Zero");
                }

                if (match.Groups[5].Value == "/")
                    res = a / b;
                if (match.Groups[5].Value == "+")
                    res = a + b;
                if (match.Groups[5].Value == "-")
                    res = a - b;

                bReplace = true;

                return res.ToString();
            });
        } while (bReplace);

        return strOut;
    }
}



