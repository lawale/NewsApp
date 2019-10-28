using System;
using NewsApp.Core.ViewModel;
using Xamarin.Forms;

namespace NewsApp.Core.Factories
{
    public interface IViewFactory
    {
        void Register<TView>(string viewKey = null) where TView : Page;
        void Register<TViewModel, TView>(string viewKey = null);
        Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null, bool setBindingContext = true) where TViewModel : class, IViewModel;
        Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null, string viewKey = null, bool setBindingContext = true) where TViewModel : class, IViewModel;
        Page Resolve<TViewModel>(TViewModel viewModel, bool setBindingContext = true) where TViewModel : class, IViewModel;
    }
}