using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorBlank
{
    internal class LoggerException : Exception
    {
        public string Message { get; set; }

        public LoggerException(string message)
        {
            Message = message;
        }
    }
}
