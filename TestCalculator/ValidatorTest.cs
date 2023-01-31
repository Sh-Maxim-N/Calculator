using Calculator.Services;
using Validator = Calculator.Services.Validator;
using Calculator.Interfaces;
using Moq;
using Calculator;


namespace TestCalculator;

[TestClass]
public class ValidatorTest
{
    Validator validator;

    private readonly Mock<IValidator> _validatorMock = new();

    private string LastWrite = null;
    private string ConsoleResult = null;

    [TestMethod]
    [DataRow("2/2+2*2-1", true)]
    [DataRow("1+2*(3+2)", true)]
    [DataRow("(2+2)*-2+15", true)]
    [DataRow("2*(3*(5-3))", true)]
    [DataRow("(5+3)*3-(88/8-7)", true)]
    [DataRow("10", true)]
    [DataRow("(5/2)+(100/2*(3+-5))", true)]
    [DataRow("(51+9*3)/6", true)]
    [DataRow("((2*(50/5)+8)-4)/6", true)]
    [DataRow("3", true)]
    [DataRow("(1)", true)]
    [DataRow("1+(1)+1", true)]
    [DataRow("(-5)", true)]
    [DataRow("-(5)", true)]
    [DataRow("33+(2-5)", true)]
    [DataRow("12+(-12)", true)]
    [DataRow("1+2*(3+2)", true)]
    [DataRow("(2+3)-(4+3)", true)]
    [DataRow("12+((((12)+8)+7)+6)", true)]
    [DataRow("12+(3-(8+5))+1", true)]


    public void TestValidatorFilePassed(string testString, bool expectedResult)
    {
        validator = new Validator();
        bool result = validator.CheckIfExpressionsInFileValid(testString);

        Assert.AreEqual(expectedResult, result);
    }


    [TestMethod]

    [DataRow("2//2+2*2-1", false)]
    [DataRow("(1+1)+1=3", false)]
    [DataRow("6-6=-0", false)]
    [DataRow("1+2*(3+2))", false)]
    [DataRow("((2+2)*-2+15", false)]
    [DataRow("2*(3*(*(5-3))", false)]
    [DataRow("(5+3)*3-()(88/8-7)", false)]
    [DataRow("(5+Niko)*3-(88/8-7)", false)]
    [DataRow("", false)]
    [DataRow(null, false)]
    [DataRow("(a+3)-4", false)]
    [DataRow("20O0", false)]
    

    public void TestValidatorFileNotPassed(string testString, bool expectedResult)
    {
        validator = new Validator();
        bool result = validator.CheckIfExpressionsInFileValid(testString);

        Assert.AreEqual(expectedResult, result);
    }


    [TestMethod]

    [DataRow(true, "0")]
    [DataRow(true, "-3")]
    [DataRow(true, "3*4")]
    [DataRow(true, "1+2+3*4+5+6")]
    [DataRow(true, "1+2-3*4+5+6")]
    [DataRow(true, "-8/2")]
    [DataRow(true, "2+3+4+2+1")]
    [DataRow(true, "2+3-4+2-1")]
    [DataRow(true, "2+3-4*2-1")]
    [DataRow(true, "2+3-4/2-1")]
    [DataRow(true, "2+15/3+4*2")]
    [DataRow(true, "1+2+3*-4+5+6")]
    [DataRow(true, "1+2-3*-4+5+6")]
    [DataRow(true, "8/-2")]
    [DataRow(true, "-8/-2")]
    [DataRow(true, "9/2")]
    [DataRow(true, "2*11")]
    [DataRow(true, "12--12")]
    [DataRow(true, "-12--12")]
    [DataRow(true, "2+3-4/-2-1+73")]
    [DataRow(true, "2+3+4/-2/2-1")]
    [DataRow(true, "2+35/2+14")]
    [DataRow(true, "2-36/9-3+15")]


    public void TestValidatorConsolePassed(bool expectedResult, string testString)
    {
        validator = new Validator();
        bool result = validator.CheckIfConsoleExpressionValid(testString);

        Assert.AreEqual(expectedResult, result);
    }

    [TestMethod]

    [DataRow(false, "")]
    [DataRow(false, null)]
    [DataRow(false, "(1+1)+1")]
    [DataRow(false, "7****8")]
    [DataRow(false, "4-+*8")]
    [DataRow(false, "15-5/*8")]
    [DataRow(false, "1*2+x-8")]
    [DataRow(false, "10O0")]
    [DataRow(false, "5-5=-0")]

    public void TestValidatorConsoleNotPassed(bool expectedResult, string testString)
    {
        validator = new Validator();
        bool result = validator.CheckIfConsoleExpressionValid(testString);

        Assert.AreEqual(expectedResult, result);
    }

}
