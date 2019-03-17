using NewsApp.Core.Extensions;
using NewsApp.Core.Factories;
using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Core.Services
{
    public abstract class Bootstrapper
    {

        IViewFactory viewFactory = DependencyService.Get<IViewFactory>();
        INavigationImpl navigation = DependencyService.Get<INavigationImpl>();

        /// <summary>
        /// This method sets up the app navigation that does not use Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the RootPage of the app</typeparam>
        /// <param name="viewModel">ViewModel for the rootpage</param>
        public void StartApp<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            RegisterViews(viewFactory);
            SetupBasicApp(viewModel);
        }

        /// <summary>
        /// This method sets up the app navigation that uses Master Detail as the root
        /// </summary>
        /// <typeparam name="TViewModel">The type of IViewModel that backs the MasterPage of the app</typeparam>
        /// <typeparam name="RViewModel">The type of IViewModel that backs the DetailPage of the app</typeparam>
        /// <param name="masterViewModel">The viewmodel for masterpage</param>
        /// <param name="initialDetailViewModel">The viewmodel for the detailpage</param>
        /// <param name="masterBehavior">The MasterBehavior for the MasterDetailPage</param>
        /// <param name="masterDetailSetting">Setting for for Navigation after Navigation in the Master Detail</param>
        public void StartAsMasterDetail<TViewModel, RViewModel>(TViewModel masterViewModel, RViewModel initialDetailViewModel, MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation) where TViewModel : class, IViewModel where RViewModel : class, IViewModel
        {
            RegisterViews(viewFactory);
            SetupMasterDetailApp(masterViewModel, initialDetailViewModel, masterBehavior, masterDetailSetting);
        }
        
        protected abstract void RegisterViews(IViewFactory viewFactory);
        
        private void SetupMasterDetailApp<TViewModel, RViewModel>(TViewModel masterViewModel, RViewModel initialDetailViewModel, MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation) where TViewModel : class, IViewModel where RViewModel : class, IViewModel
        {
            var master = viewFactory.Resolve(masterViewModel);
            var detail = viewFactory.Resolve(initialDetailViewModel);
            navigation.SetMasterDetail(master, detail, masterBehavior, masterDetailSetting);
        }
        
        private void SetupBasicApp<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var page = viewFactory.Resolve(viewModel);
            navigation.SetMainPage(page);
        }
    }
}
