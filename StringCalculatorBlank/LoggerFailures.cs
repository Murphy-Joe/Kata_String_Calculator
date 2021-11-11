using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StringCalculatorBlank
{
    public class LoggerFailures
    {
        private readonly StringCalculator _calculator;
        private Mock<ILogger> _loggerStub;
        private Mock<IWebService> _webService;

        public LoggerFailures()
        {
            _webService = new Mock<IWebService>();
            _loggerStub = new Mock<ILogger>();
            _calculator = new StringCalculator(_loggerStub.Object, _webService.Object);
            
        }

        [Fact]
        public void ShouldCallWebServiceWhenLoggerThrows()
        {
            _loggerStub.Setup(i => i.Write(It.IsAny<string>())).Throws(new LoggerException("whamo"));

            _calculator.Add("1");

            _webService.Verify(i => i.Notify("whamo"));
        }

        [Fact]
        public void ServiceNotCalledIfNoException()
        {
            _calculator.Add("1,2,3");

            _webService.Verify(i => i.Notify(It.IsAny<string>()), Times.Never());
        }
    }
}
