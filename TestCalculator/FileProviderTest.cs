using Calculator.Services;
using Validator = Calculator.Services.Validator;
using Calculator.Interfaces;
using Moq;
using Calculator;


namespace TestCalculator;

[TestClass]
public class FileProviderTest
{
    FileProvider fileProvider;
    Validator validator;
    
    private readonly Mock<IFileService> _fileMock = new();
    private readonly Mock<IValidator> _validatorMock = new();

    private string LastWrite = null;
    private string ConsoleResult = null;

    [TestMethod]
    [DataRow("2/2+2*2-1", "4")]
    [DataRow("1+2*(3+2)", "11")]
    [DataRow("(2+2)*-2+15", "7")]
    [DataRow("2*(3*(5-3))", "12")]
    [DataRow("(5+3)*3-(88/8-7)", "20")]
    [DataRow("10", "10")]
    [DataRow("(5/2)+(100/2*(3+-5))", "-97,5")]
    [DataRow("(51+9*3)/6", "13")]
    [DataRow("((2*(50/5)+8)-4)/6", "4")]
    [DataRow("3", "3")]
    [DataRow("(1)", "1")]
    [DataRow("1+(1)+1", "3")]
    [DataRow("(-5)", "-5")]
    [DataRow("-(5)", "-5")]
    [DataRow("33+(2-5)", "30")]
    [DataRow("12+(-12)", "0")]
    [DataRow("1+2*(3+2)", "11")]
    [DataRow("(2+3)-(4+3)", "-2")]
    [DataRow("12+((((12)+8)+7)+6)", "45")]
    [DataRow("12+(3-(8+5))+1", "3")]


    public void TestFileProviderPassed(string testString, string expectedValue)
    {

        this._validatorMock.Setup(x => x.CheckIfExpressionsInFileValid(It.IsAny<string>()))
            .Returns(true);

        List<string> outLines = null;
        List<string> expectedLines = new List<string>() { expectedValue };
        _fileMock.Setup(x => x.GetStringsFromFile(It.IsAny<string>())).Returns(new string[] { testString });
        _fileMock.Setup(x => x.FileExist(It.IsAny<string>())).Returns(true);
        _fileMock.Setup(x => x.WriteLines(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Callback((string filename, List<string> outlines) => outLines = outlines.Select(y => y.Substring(y.IndexOf("=") + 2)).ToList());

        fileProvider = new FileProvider(_validatorMock.Object, _fileMock.Object, "c:\\notexist.txt");

        fileProvider.TemplateMethod();

        Assert.AreEqual(expectedLines[0], outLines[0]);
    }

    [TestMethod]

    [DataRow("6-6/(2-2)", "Error")]
    [DataRow("2//2+2*2-1", "Error")]
    [DataRow("(1+1)+1=3", "Error")]
    [DataRow("6-6=-0", "Error")]
    [DataRow("1+2*(3+2))", "Error")]
    [DataRow("((2+2)*-2+15", "Error")]
    [DataRow("2*(3*(*(5-3))", "Error")]
    [DataRow("(5+3)*3-()(88/8-7)", "Error")]
    [DataRow("(5+Niko)*3-(88/8-7)", "Error")]
    [DataRow("", "Error")]
    [DataRow(null, "Error")]
    [DataRow("(a+3)-4", "Error")]
    [DataRow("20O0", "Error")]
    [DataRow("6-6/(2-(45+(45)-))", "Error")]


    public void TestFileProviderNotPassed(string testString, string expectedValue)
    {

        this._validatorMock.Setup(x => x.CheckIfConsoleExpressionValid(It.IsAny<string>()))
            .Returns(true);

        List<string> outLines = null;
        List<string> expectedLines = new List<string>() { expectedValue };
        _fileMock.Setup(x => x.GetStringsFromFile(It.IsAny<string>())).Returns(new string[] { testString });
        _fileMock.Setup(x => x.FileExist(It.IsAny<string>())).Returns(true);
        _fileMock.Setup(x => x.WriteLines(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Callback((string filename, List<string> outlines) => outLines = outlines.Select(y => y.Substring(y.IndexOf("=") + 2)).ToList());

        fileProvider = new FileProvider(_validatorMock.Object, _fileMock.Object, "c:\\notexist.txt");
        fileProvider.TemplateMethod();
        

        Assert.AreEqual(expectedLines[0], outLines[0]);
    }
}