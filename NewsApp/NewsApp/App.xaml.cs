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
        private readonly IServices services;
        private MenuPage MenuPage;
		public App ()
		{
			InitializeComponent();
            HomePage = new HomePage();
            services = new Service(ref HomePage, ref NavigationPage);
            MenuPage = new MenuPage(new MenuViewModel(services), services) { Title = "Categories" };
            //NavigationPage = new NavigationPage(new View1(services));
            NavigationPage = new NavigationPage(new ArticlesPage(new NewsViewModel(new NewsCategory { CategoryName = topstories }, services)));
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
