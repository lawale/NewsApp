using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Core.Factories;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;

[assembly: Dependency(typeof(ViewFactory))]
namespace NewsApp.Core.Factories
{
    public class ViewFactory : IViewFactory
    {
        readonly IDictionary<Type, Type> map = new Dictionary<Type, Type>();
        readonly object sync = new object();

        private TViewModel CreateViewModel<TViewModel>(object parameter = null) where TViewModel : class, IViewModel
        {
            Type type = typeof(TViewModel);
            if (parameter == null)
                return Activator.CreateInstance(type) as TViewModel;
            else
            {
                ConstructorInfo constructor = type.GetTypeInfo()
                    .DeclaredConstructors
                    .FirstOrDefault(c =>
                    {
                        var parameterInfos = c.GetParameters();
                        return parameterInfos.Length == 1 && parameterInfos[0].ParameterType == parameter.GetType();
                    });
                var parameters = new object[] { parameter };
                if (constructor == null)
                    throw new Exception($"No suitable constructor exists for class {type}");
                return constructor.Invoke(parameters) as TViewModel;
            }
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel where TView : Page
        {
            lock(sync)
            {
                if (map.ContainsKey(typeof(TViewModel)))
                    throw new Exception($"{typeof(TViewModel)} has already been registered with {map[typeof(TView)]}");
                map[typeof(TViewModel)] = typeof(TView);
            }

        }

        public Page Resolve<TViewModel>(Action<TViewModel> setStateAction, object parameter = null)
            where TViewModel : class, IViewModel
            => Resolve(out TViewModel viewModel, setStateAction, parameter);

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction, object parameter = null) where TViewModel : class, IViewModel
        {
            viewModel = CreateViewModel<TViewModel>(parameter);

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
