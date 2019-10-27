using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NewsApp.View;
using NewsApp.Core.ViewModel;
using NewsApp.Core.Extensions;
using static NewsApp.Core.Helpers.Categories;
using NewsApp.Core.Model;
using System.Collections.Generic;
using XF.Material.Forms;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NewsApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            PackageInit();
            SetupPages();
        }




        /// <summary>
        /// Init start-up pages here
        /// </summary>
        void SetupPages()
        {
            var vm = new NewsViewModel(new NewsCategory { CategoryName = topstories });
            var menu = new MenuViewModel();
            var bootstrapper = new NewsAppBootsrap();
            bootstrapper.StartAsMasterDetail(menu, vm);
            //var page = new ArticlesPage { BindingContext = vm };
            //services.CurrentPage = page;
            //var nav = new NavigationPage();
            //nav.PushAsync(page);
            //HomePage = new HomePage
            //{
            //    Master = new MenuPage(),
            //    Detail = nav
            //};
        }

        /// <summary>
        /// Init 3rd-Party libraries here
        /// </summary>
        void PackageInit()
        {
            DLToolkit.Forms.Controls.FlowListView.Init();
            Material.Init(this);
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


