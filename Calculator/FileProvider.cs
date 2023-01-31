using Calculator.Interfaces;
using Calculator.Services;

namespace Calculator;

public class FileProvider : CalculatorManager
{
    private string[] LinesInputArray;
    private string CurrentLine;
    private readonly string _pathInput;
    private string _originalInputLine;
    private IFileService _writeResult;
    private IValidator _validator;

    private readonly IFileService _fileService; 

    public FileProvider(IValidator validator, IFileService stringGetter, string input) 
    {
        this._validator = validator ?? throw new ArgumentNullException(nameof(validator));
        this._fileService = stringGetter ?? throw new ArgumentNullException(nameof(stringGetter));
        this._pathInput = input;
    }

    protected override void ReadFileOrExpression()
    {
        if (_fileService.FileExist(_pathInput) & _fileService.GetStringsFromFile(_pathInput) != null)
        {
            LinesInputArray = _fileService.GetStringsFromFile(_pathInput); 
        }

        ProcessExpression();
    }

    protected override void ProcessExpression()
    {
        string outputPath = _pathInput.Replace(".txt", "_result.txt");
        List<string> result = new();

        for (int i = 0; i < LinesInputArray.Length; i++)
        {
            _originalInputLine = LinesInputArray[i];
            CurrentLine = LinesInputArray[i];
            string outputLine;

            if (_validator.CheckIfExpressionsInFileValid(LinesInputArray[i]))
            {
                AreAllBracketsExpunded();

                CurrentLine = CalculationWithMathPriority(CurrentLine);

                outputLine = $"{_originalInputLine} = {CurrentLine}";

                result.Add(outputLine);
            }

            outputLine = Messages.FileStringError;
            result.Add(outputLine);
        }

        _fileService.WriteLines(outputPath, result);
    }

    private void AreAllBracketsExpunded()
    {
        while (CurrentLine.Contains('('))
            CurrentLine = ExpundBrackets(CurrentLine);
    }

    private string ExpundBrackets(string strExpression)
    {
        if (!strExpression.Contains("("))
        {
            return CalculationWithMathPriority(strExpression);
        }

        var lastIndexOfOpenBrace = strExpression.LastIndexOf('(') + 1;
        var firstIndexOfClosetBrace = strExpression.IndexOf(')', lastIndexOfOpenBrace);

        var expressionInBraces = strExpression.Substring(lastIndexOfOpenBrace, firstIndexOfClosetBrace - lastIndexOfOpenBrace);
        var expressionWithBraces = strExpression.Substring(lastIndexOfOpenBrace - 1, firstIndexOfClosetBrace - lastIndexOfOpenBrace + 2);

        var resultOfexpressionInBraces = CalculationWithMathPriority(expressionInBraces);

        var expressionAfterReplace = strExpression.Replace(expressionWithBraces, resultOfexpressionInBraces);

        return ExpundBrackets(expressionAfterReplace);

    }
}