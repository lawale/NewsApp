using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Core.Extensions
{
    public interface IToast
    {
        void Show(string message);
    }
}
