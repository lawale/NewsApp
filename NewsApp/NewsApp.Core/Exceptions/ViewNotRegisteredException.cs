using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Exceptions
{
    public class ViewNotRegisteredException : Exception
    {
        public ViewNotRegisteredException() { }

        public ViewNotRegisteredException(string message)
            : base(message) { }

        public ViewNotRegisteredException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
