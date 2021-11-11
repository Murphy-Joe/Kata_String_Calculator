using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringCalculatorBlank
{
    public class StringCalcLogsAnswer
    {
        private readonly Mock<ILogger> _moqLogger;
        private readonly StringCalculator _calculator;

        public StringCalcLogsAnswer()
        {
            _moqLogger = new Mock<ILogger>();
            _calculator = new StringCalculator(_moqLogger.Object, new Mock<IWebService>().Object);
        }

        [Theory]
        [InlineData("1,2", "3")]
        public void AnswerIsLogged(string nums, string exp)
        {
            _calculator.Add(nums);
            _moqLogger.Verify(i => i.Write(exp));
        }
    }
}
