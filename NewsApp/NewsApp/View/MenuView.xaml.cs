using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Core.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuView : ContentPage
	{
        //readonly IServices services = DependencyService.Get<IServices>();
		public MenuView ()
		{
			InitializeComponent ();
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