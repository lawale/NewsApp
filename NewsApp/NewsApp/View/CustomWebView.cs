using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NewsApp.View
{
    public class CustomWebView : WebView
    {
        public static readonly BindableProperty BackProperty =
            BindableProperty.Create(nameof(Back), typeof(ICommand), typeof(CustomWebView),null,BindingMode.OneWayToSource);

        public static readonly BindableProperty ForwardProperty =
            BindableProperty.Create(nameof(Back), typeof(ICommand), typeof(CustomWebView),null,BindingMode.OneWayToSource);

        //public static readonly BindableProperty CanMoveBackProperty =
        //    BindableProperty.Create(nameof(CanMoveBack), typeof(bool), typeof(CustomWebView), null, BindingMode.OneWayToSource);

        //public static readonly BindableProperty CanMoveForwardProperty =
        //    BindableProperty.Create(nameof(CanMoveForward), typeof(bool), typeof(CustomWebView), null, BindingMode.OneWayToSource);
        

        public CustomWebView()
        {
            Back = new Command(GoBack);
            Forward = new Command(GoForward);
        }

        //public bool CanMoveBack
        //{
        //    get { return (bool)GetValue(CanMoveBackProperty); }
        //    set { SetValue(CanMoveBackProperty, CanGoBack); }
        //}

        //public bool CanMoveForward
        //{
        //    get { return (bool)GetValue(CanMoveForwardProperty); }
        //    set { SetValue(CanMoveForwardProperty, CanGoForward); }
        //}

        public ICommand Forward
        {
            get { return (ICommand)GetValue(ForwardProperty); }
            set { SetValue(ForwardProperty, value); }
        }

        public ICommand Back
        {
            get { return (ICommand)GetValue(BackProperty); }
            set { SetValue(BackProperty, value); }
        }
    }
}
