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
using NewsApp.Core;

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
            var detail_vm = new ArticlesViewModel(new NewsCategory { CategoryName = topstories });
            var master_vm = new MenuViewModel();
            var bootstrapper = new NewsAppBootsrap();
            bootstrapper.SetupMasterDetailApp(master_vm, detail_vm);
        }

        /// <summary>
        /// Init 3rd-Party libraries here
        /// </summary>
        void PackageInit()
        {
            Material.Init(this);
            Startup.Init();
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


