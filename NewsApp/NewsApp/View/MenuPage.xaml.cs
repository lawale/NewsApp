using NewsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        IServices services;
		public MenuPage (MenuViewModel viewModel, IServices service)
		{
            services = service;
			InitializeComponent ();
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.LoadCategory.Execute(null);
        }

        private MenuViewModel ViewModel => BindingContext as MenuViewModel;
    }
}