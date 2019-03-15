using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using NewsApp.Core.Extensions;
using UIKit;
using Xamarin.Toast;
using Xamarin.Forms;

[assembly: Dependency(typeof(NewsApp.iOS.Extensions.Toast))]
namespace NewsApp.iOS.Extensions
{
    public class Toast : IToast
    {
        public void Show(string message)
        {
            var View = new UIView();
            View.MakeToast(message);
        }
    }
}