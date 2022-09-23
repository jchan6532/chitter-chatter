using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPHelpers.Exceptions
{
    public class LogFileNotExistException : Exception
    {
        public LogFileNotExistException(string message) : base(message)
        {
        }
    }
}
