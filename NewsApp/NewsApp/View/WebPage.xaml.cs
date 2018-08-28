using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewsApp.ViewModel;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebPage : ContentPage
	{
		public WebPage (WebViewModel model)
		{
			InitializeComponent ();
            BindingContext = model;
		}

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e) => ViewModel.SetLoading(false);

        private WebViewModel ViewModel => BindingContext as WebViewModel;

        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.CanGoBack)
            {
                ViewModel.GoBackCommand.Execute(null);
                return true;
            }
            else
                return base.OnBackButtonPressed();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.CanGoForward)
            {
                ViewModel.GoForwardCommand.Execute(null);
            }
            else
                return;
        }
    }
}