using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NewsApp.Core.Extensions;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(NewsApp.Droid.Extensions.Toast))]
namespace NewsApp.Droid.Extensions
{
    public class Toast : IToast
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(CrossCurrentActivity.Current.AppContext, message, ToastLength.Long).Show();
        }
    }
}