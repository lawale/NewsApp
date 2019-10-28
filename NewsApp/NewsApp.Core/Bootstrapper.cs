using NewsApp.Core.Extensions;
using NewsApp.Core.Factories;
using NewsApp.Core.Services;
using NewsApp.Core.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Core
{
    public abstract class Bootstrapper
    {
        private readonly IViewFactory viewFactory = null;

        public MasterDetailSetting MasterDetailSetting { get; set; }

        protected Bootstrapper()
        {
            this.viewFactory = Startup.ServiceProvider.GetService<IViewFactory>();
            RegisterViews(viewFactory);
        }

        protected abstract void RegisterViews(IViewFactory viewFactory);

        /// <summary>
        /// This method sets up the app navigation that uses Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the MasterPage of the app</typeparam>
        /// <typeparam name="RViewModel">The type of IViewModel that backs the DetailPage of the app</typeparam>
        /// <param name="masterBehavior">The MasterBehavior for the MasterDetailPage</param>
        /// <param name="masterDetailSetting">Option for for Navigation after Navigation in the Master Detail</param>
        /// <param name="wrapDetailWithNavigation">Option for wrapping the detail in a Navigation Page</param>
        public void SetupMasterDetailApp<TViewModel, RViewModel>(MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation, bool wrapDetailWithNavigation = true) where TViewModel : class, IViewModel where RViewModel : class, IViewModel
        {
            var masterViewModel = Startup.ServiceProvider.GetService<TViewModel>();
            var detailViewModel = Startup.ServiceProvider.GetService<RViewModel>();
            SetupMasterDetailApp(masterViewModel, detailViewModel, masterBehavior, masterDetailSetting, wrapDetailWithNavigation);
        }

        /// <summary>
        /// This method sets up the app navigation that uses Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the MasterPage of the app</typeparam>
        /// <typeparam name="RViewModel">The type of IViewModel that backs the DetailPage of the app</typeparam>
        /// <param name="masterViewModel">The viewmodel for masterpage</param>
        /// <param name="initialDetailViewModel">The viewmodel for the detailpage</param>
        /// <param name="masterBehavior">The MasterBehavior for the MasterDetailPage</param>
        /// <param name="masterDetailSetting">Option for for Navigation after Navigation in the Master Detail</param>
        /// <param name="wrapDetailWithNavigation">Option for wrapping the detail in a Navigation Page</param>
        public void SetupMasterDetailApp<TViewModel, RViewModel>(TViewModel masterViewModel, RViewModel initialDetailViewModel, MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation, bool wrapDetaiWithNavigation = true) where TViewModel : class, IViewModel where RViewModel : class, IViewModel
        {
            var master = viewFactory.Resolve(masterViewModel);
            var detail = wrapDetaiWithNavigation
                ? new NavigationPage(viewFactory.Resolve(initialDetailViewModel))
                : viewFactory.Resolve(initialDetailViewModel);

            if (string.IsNullOrWhiteSpace(master.Title))
                master.Title = "Menu";

            var masterDetailPage = new MasterDetailPage
            {
                Master = master,
                Detail = detail,
                MasterBehavior = masterBehavior
            };
            MasterDetailSetting = masterDetailSetting;
            Application.Current.MainPage = masterDetailPage;
        }

        /// <summary>
        /// This method sets up the app navigation that does not use Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the RootPage of the app</typeparam>
        /// <param name="viewModel">ViewModel for the rootpage</param>
        /// <param name="wrapWithNavigation">Option for wrapping in a Navigation Page</param>
        private void SetupBasicApp<TViewModel>(TViewModel viewModel, bool wrapWithNavigation = true) where TViewModel : class, IViewModel
        {
            var page =
                wrapWithNavigation
                ? new NavigationPage(viewFactory.Resolve(viewModel))
                : viewFactory.Resolve(viewModel);

            Application.Current.MainPage = page;
        }

        /// <summary>
        /// This method sets up the app navigation that does not use Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the RootPage of the app</typeparam>
        /// <param name="wrapWithNavigation">Option for wrapping in a Navigation Page</param>
        public void SetupBasicApp<TViewModel>(bool wrapWithNavigation = true) where TViewModel : class, IViewModel
        {
            var page =
                wrapWithNavigation
                ? new NavigationPage(viewFactory.Resolve<TViewModel>())
                : viewFactory.Resolve<TViewModel>();
        }
    }
}
