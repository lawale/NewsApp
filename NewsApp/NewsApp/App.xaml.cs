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
        private NavigationPage NavigationPage;
        private readonly IServices services = DependencyService.Get<IServices>();
        private MenuPage MenuPage;
		public App ()
		{
			InitializeComponent();
            DLToolkit.Forms.Controls.FlowListView.Init();
            HomePage = new HomePage();
            MenuPage = new MenuPage{ Title = "Categories", BindingContext = new MenuViewModel() };
            var cat = new NewsCategory { CategoryName = topstories };
            var vm = new NewsViewModel(cat);
            var page = new ArticlesPage{ BindingContext = vm };
            NavigationPage = new NavigationPage(page);
            HomePage.Master = MenuPage;
            HomePage.Detail = NavigationPage;
            MainPage = HomePage;
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
