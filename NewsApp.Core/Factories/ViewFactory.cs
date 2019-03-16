using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Core.Factories;
using Xamarin.Forms;

[assembly: Dependency(typeof(ViewFactory))]
namespace NewsApp.Core.Factories
{
    public class ViewFactory : IViewFactory
    {
        readonly IDictionary<Type, Type> map = new Dictionary<Type, Type>();

        public void Register<TViewModel, TView>() 
            where TViewModel : class, IViewModel where TView : Page
            => map[typeof(TViewModel)] = typeof(TView);

        public Page Resolve<TViewModel>(Action<TViewModel> setStateAction)
            where TViewModel : class, IViewModel
            => Resolve(out TViewModel viewModel, setStateAction);

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction) where TViewModel : class, IViewModel
        {
            viewModel = DependencyService.Resolve<TViewModel>();
            var viewType = map[typeof(TViewModel)];
            var view = Activator.CreateInstance(viewType) as Page;
            if (setStateAction != null)
                viewModel.SetState(setStateAction);
            view.BindingContext = viewModel;
            return view;
        }

        public Page Resolve<TViewModel>(TViewModel viewModel) where TViewModel : class,IViewModel
        {
            var viewType = map[typeof(TViewModel)];
            var view = Activator.CreateInstance(viewType) as Page;
            view.BindingContext = viewModel;
            return view;
        }
    }
}
