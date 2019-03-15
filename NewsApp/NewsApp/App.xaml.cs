using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NewsApp.View;
using NewsApp.ViewModel;
using NewsApp.Extensions;
using static NewsApp.Helpers.Categories;
using NewsApp.Model;
using System.Collections.Generic;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NewsApp
{
	public partial class App : Application
	{
        private HomePage HomePage;
        private readonly IServices services = DependencyService.Get<IServices>();
		public App ()
		{
			InitializeComponent();
            PackageInit();
            SetupPages();
            MainPage = HomePage;
		}

        /// <summary>
        /// Init start-up pages here
        /// </summary>
        void SetupPages()
        {
            var vm = new NewsViewModel(new NewsCategory { CategoryName = topstories });
            var page = new ArticlesPage { BindingContext = vm };
            HomePage = new HomePage
            {
                Master = new MenuPage(),
                Detail = new NavigationPage(page)
            };
        }

        /// <summary>
        /// Init 3rd-Party libraries here
        /// </summary>
        void PackageInit()
        {
            DLToolkit.Forms.Controls.FlowListView.Init();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
