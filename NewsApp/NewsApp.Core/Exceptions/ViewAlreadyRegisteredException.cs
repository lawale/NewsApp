using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Exceptions
{
    public class ViewAlreadyRegisteredException : Exception
    {
        public ViewAlreadyRegisteredException() { }

        public ViewAlreadyRegisteredException(string message)
            : base(message) { }

        public ViewAlreadyRegisteredException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
