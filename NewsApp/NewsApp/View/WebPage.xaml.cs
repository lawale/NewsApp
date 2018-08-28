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
        private void IView_Navigating(object sender, WebNavigatingEventArgs e) => ViewModel.SetLoading(true);

        private WebViewModel ViewModel => BindingContext as WebViewModel;

        protected override bool OnBackButtonPressed()
        {
            if (IView.CanGoBack)
            {
                IView.GoBack();
                return true;
            }
            else
                return base.OnBackButtonPressed();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (IView.CanGoForward)
            {
                IView.GoForward();
            }
            else
                return;
        }
    }
}