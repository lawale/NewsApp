using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsApp.Core.ViewModel;
using NewsApp.Core.Services;
using Xamarin.Forms;
using NewsApp.Core.Factories;

[assembly: Xamarin.Forms.Dependency(typeof(Navigator))]
namespace NewsApp.Core.Services
{
    public class Navigator : INavigator
    {
        readonly Lazy<INavigation> navigation;
        readonly IViewFactory viewFactory;

        INavigation Navigation => navigation.Value;

        public Navigator()
        {
            viewFactory = DependencyService.Get<IViewFactory>();
            navigation = DependencyService.Resolve<Lazy<INavigation>>();
        }

        public async Task<IViewModel> PopAsync()
        {
            var page = await Navigation.PopAsync();
            return page.BindingContext as IViewModel;
        }

        public async Task<IViewModel> PopModalAsync()
        {
            var page = await Navigation.PopModalAsync();
            return page.BindingContext as IViewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(out TViewModel viewModel, setStateAction);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(viewModel);
            await Navigation.PushAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(out TViewModel viewModel, setStateAction);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel
        {
            var view = viewFactory.Resolve(viewModel);
            await Navigation.PushModalAsync(view);
            return viewModel;
        }

        public async Task PopToRootAsync()
        {
            await Navigation.PopToRootAsync();
        }
    }
}
