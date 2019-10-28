using Microsoft.Extensions.Logging;
using NewsApp.Core.Factories;
using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsApp.Core.Services
{
    public class NavigationService : INavigationService
    {
        private readonly ILogger<NavigationService> logger;

        private readonly IViewFactory viewFactory;

        public NavigationService(ILogger<NavigationService> logger, IViewFactory viewFactory)
        {
            this.logger = logger;
            this.viewFactory = viewFactory;
        }

        public async Task NavigateForward<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(viewModel);
            await CurrentPage().Navigation.PushAsync(view);
            logger.LogInformation($"Completed Navigation to {view.GetType().Name}");
        }

        public async Task NavigateForward<TViewModel>(bool setBindingContext = true) where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve<TViewModel>();
            await CurrentPage().Navigation.PushAsync(view);
            logger.LogInformation($"Completed Navigation to {view.GetType().Name}");
        }

        public async Task ModalNavigateForward<TViewModel>(bool setBindingContext = true) where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve<TViewModel>();
            await CurrentPage().Navigation.PushModalAsync(view);
            logger.LogInformation($"Completed Navigation to {view.GetType().Name}");
        }

        public async Task ModalNavigateForward<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(viewModel);
            await CurrentPage().Navigation.PushModalAsync(view);
            logger.LogInformation($"Completed Navigation to {view.GetType().Name}");
        }

        public async Task ChangeDetail<TViewModel>() where TViewModel : class, IViewModel
        {
            var navPage = (Application.Current.MainPage as MasterDetailPage).Detail;
            var rootPage = navPage.Navigation.NavigationStack.FirstOrDefault();
            var page = viewFactory.Resolve<TViewModel>();
            rootPage.Navigation.InsertPageBefore(page, rootPage);
            await rootPage.Navigation.PopToRootAsync();
        }

        public async Task ChangeDetail<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel
        {
            var navPage = (Application.Current.MainPage as MasterDetailPage).Detail;
            var rootPage = navPage.Navigation.NavigationStack.FirstOrDefault();
            var page = viewFactory.Resolve(viewModel);
            rootPage.Navigation.InsertPageBefore(page, rootPage);
            await rootPage.Navigation.PopToRootAsync();
        }

        public async Task<object> ModalNavigateBackward() => (await CurrentPage().Navigation.PopModalAsync(true)).BindingContext;

        public async Task<object> NavigateBackward() => (await CurrentPage().Navigation.PopAsync(true)).BindingContext;

        public async Task NavigateBackwardToRoot() => await CurrentPage().Navigation.PopToRootAsync(true);

        private Page CurrentPage() => Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

    }
}
