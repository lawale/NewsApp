using NewsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlesPage : ContentPage
	{
		public ArticlesPage (NewsViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            if(ViewModel.Loading)
                ViewModel.LoadArticles.Execute(null);
            base.OnAppearing();
        }

        private NewsViewModel ViewModel
        {
            get
            {
                return BindingContext as NewsViewModel;
            }
        } 

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectArticle.Execute(null);
        }
    }
}