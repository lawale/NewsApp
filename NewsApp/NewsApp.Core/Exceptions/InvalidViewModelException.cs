using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Exceptions
{
    public class InvalidViewModelException : Exception
    {
        public InvalidViewModelException() { }

        public InvalidViewModelException(string message)
            : base(message) { }

        public InvalidViewModelException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
