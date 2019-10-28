using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Exceptions
{
    public class InvalidViewException : Exception
    {
        public InvalidViewException() { }

        public InvalidViewException(string message)
            : base(message) { }

        public InvalidViewException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
