using NewsApp.Core.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewsApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlesPage : ContentPage
	{
		public ArticlesPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            if (!ViewModel.IsRefreshing && !ViewModel.HasLoaded)
                list.RefreshCommand.Execute(null);
            base.OnAppearing();
        }

        private NewsViewModel ViewModel => BindingContext as NewsViewModel;

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var image = sender as ExtendedImage;
        //    if (!image.HasDownloaded && image.DowloadFailed)
        //        image.ReloadImage();
            
        //}
    }
}