using Calculator.Services;
using Validator = Calculator.Services.Validator;
using Calculator.Interfaces;
using Moq;
using Calculator;


namespace TestCalculator
{
    [TestClass]
    public class ConsoleProviderTest
    {
        ConsoleProvider consoleProvider;

        private readonly Mock<IConsoleIo> _communicationMock = new();
        private readonly Mock<IFileService> _fileMock = new();
        private readonly Mock<IValidator> _validatorMock = new();
        

        private string LastWrite = null;
        private string ConsoleResult = null;

        [TestMethod, Timeout(2000)]


        [DataRow(0, "0")]
        [DataRow(-3, "-3")]
        [DataRow(12, "3*4")]
        [DataRow(26, "1+2+3*4+5+6")]
        [DataRow(2, "1+2-3*4+5+6")]
        [DataRow(-4, "-8/2")]
        [DataRow(12, "2+3+4+2+1")]
        [DataRow(2, "2+3-4+2-1")]
        [DataRow(-4, "2+3-4*2-1")]
        [DataRow(2, "2+3-4/2-1")]
        [DataRow(15, "2+15/3+4*2")]
        [DataRow(2, "1+2+3*-4+5+6")]
        [DataRow(26, "1+2-3*-4+5+6")]
        [DataRow(-4, "8/-2")]
        [DataRow(4, "-8/-2")]
        [DataRow(4.5d, "9/2")]
        [DataRow(8, "16/2")]
        [DataRow(22, "2*11")]
        [DataRow(24, "12--12")]
        [DataRow(0, "-12--12")]
        [DataRow(79, "2+3-4/-2-1+73")]
        [DataRow(3, "2+3+4/-2/2-1")]
        [DataRow(33.5d, "2+35/2+14")]
        [DataRow(9.5, "2+24/8*2+3/2")]
        [DataRow(-17, "-2+36/-9*3+3/-1")]
        [DataRow(10, "2-36/9-3+15")]

        public void TestConsoleProviderPassed(double expectedValue, string testString)
        {
            
            this._validatorMock.Setup(x => x.CheckIfConsoleExpressionValid(It.IsAny<string>()))
                .Returns(true);

            this._communicationMock.Setup(x => x.WriteLine(It.IsAny<double>()))
                .Verifiable();

            this._communicationMock.Setup(x => x.ReadLine())
                .Returns(testString);
            
            IValidator validator = new Validator();

            consoleProvider = new ConsoleProvider(validator, _communicationMock.Object);

            consoleProvider.TemplateMethod();

            this._communicationMock.Verify(x => x.WriteLine(expectedValue.ToString()), Times.Once);
        }

        [TestMethod]
        [DataRow("Invalid input. Please, enter correct expression", "")]
        [DataRow("Invalid input. Please, enter correct expression", "(1+1)+1")]
        [DataRow("Invalid input. Please, enter correct expression", "7****8")]
        [DataRow("Invalid input. Please, enter correct expression", "4-+*8")]
        [DataRow("Invalid input. Please, enter correct expression", "15-5/*8")]
        [DataRow("Invalid input. Please, enter correct expression", "1*2+x-8")]
        [DataRow("Invalid input. Please, enter correct expression", "10O0")]
        [DataRow("Invalid input. Please, enter correct expression", "5-5=-0")]

        public void TestConsoleProviderNotPassed(string expectedValue, string testString)
        {
            ConsoleResult = null;
            
            this._communicationMock.Setup(x => x.WriteLine(It.IsAny<object>()))
                .Verifiable();

            this._communicationMock.Setup(x => x.ReadLine())
                .Returns(testString);

            this._validatorMock.Setup(x => x.CheckIfConsoleExpressionValid(It.IsAny<string>()))
                .Returns(false);

            this._communicationMock.Setup(x => x.WriteLine(It.IsAny<object>()))
                .Verifiable();

            consoleProvider = new ConsoleProvider(_validatorMock.Object, _communicationMock.Object);
            
            consoleProvider.TemplateMethod();

            this._communicationMock.Verify(x => x.WriteLine(expectedValue.ToString()), Times.Once);
        }

        [TestMethod]

        public void ConsoleInputNull()
        {
            this._validatorMock.Setup(x => x.CheckIfConsoleExpressionValid(It.IsAny<string>()))
                .Returns(true);

            string input = null;
            this._communicationMock.Setup(x => x.WriteLine(It.IsAny<object>()))
                .Verifiable();

            this._communicationMock.Setup(x => x.ReadLine())
                .Returns(input);

            Assert.ThrowsException<NullReferenceException>(() => consoleProvider.TemplateMethod());
        }

    }
}