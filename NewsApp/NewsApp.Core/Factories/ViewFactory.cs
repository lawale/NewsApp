using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using NewsApp.Core.Factories;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Logging;
using NewsApp.Core.Exceptions;

namespace NewsApp.Core.Factories
{
    public class ViewFactory : IViewFactory
    {
        readonly Dictionary<string, Type> viewDictionary = new Dictionary<string, Type>();
        readonly ILogger<ViewFactory> logger = null;
        readonly object sync = new object();

        public ViewFactory(ILogger<ViewFactory> logger)
        {
            this.logger = logger;
        }

        public void Register<TViewModel, TView>(string viewKey = null)
        {
            lock (sync)
            {
                var view = typeof(TView);
                if (!view.Name.EndsWith("View"))
                {
                    var message = "View name does not follow convention used in this project.  View Names should end with \"View\".";
                    logger.LogInformation(message);
                    throw new InvalidViewException(message);
                }

                var viewModel = typeof(TViewModel);
                var key = viewModel.Name + viewKey == null ? string.Empty : $"_{viewKey}";

                if (!viewModel.Name.EndsWith("ViewModel"))
                {
                    var message = "ViewModel name does not follow convention used in this project.  ViewModel Names should end with \"ViewModel\".";
                    logger.LogInformation(message);
                    throw new InvalidViewModelException(message);
                }

                if (viewDictionary.ContainsKey(key))
                {
                    var message =
                        viewKey == null
                        ? $"The ViewModel {viewModel.Name} is already registered to a view. Please specify a key to register {view.Name} to {viewModel.Name}."
                        : $"The viewmodel key {key} is already assigned to a view. Please specify a new key.";
                    logger.LogInformation(message);
                    throw new ViewAlreadyRegisteredException(message);
                }

                viewDictionary[key] = view;

                logger.LogInformation($"{key} has been assigned as the key for registering {viewModel.Name} with view {view.Name}");
            }
        }

        public void Register<TView>(string viewKey = null)
            where TView : Page
        {
            lock (sync)
            {
                var view = typeof(TView);
                if (!view.Name.EndsWith("View"))
                {
                    var message = "View name does not follow convention used in this project.  View Names should end with \"View\".";
                    logger.LogInformation(message);
                    throw new ArgumentException(message);
                }

                var key = view.Name + "Model" + viewKey == null ? string.Empty : $"_{viewKey}";

                if (viewDictionary.ContainsKey(key))
                {
                    var message =
                        viewKey == null
                        ? $"The viewmodel {key} is already assigned to a view. Specify a key for this registration."
                        : $"The viewmodel {key} is already assigned to a view. Specify a new key for this registration.";
                    logger.LogInformation(message);
                    throw new ArgumentException(message);
                }

                viewDictionary[key] = view;

                logger.LogInformation($"{key} has been assigned as the viewmodel for view {view.Name}");
            }
        }

        public Page Resolve<TViewModel>(Action<TViewModel> setStateAction = null, bool setBindingContext = true)
            where TViewModel : class, IViewModel
            => Resolve(out TViewModel viewModel, setStateAction, null, setBindingContext);

        public Page Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null, string viewKey = null, bool setBindingContext = true) where TViewModel : class, IViewModel
        {
            viewModel = Startup.ServiceProvider.GetService<TViewModel>();
            var key = viewModel.GetType().Name + viewKey == null ? string.Empty : $"_{viewKey}";

            if (!key.EndsWith("ViewModel"))
            {
                var message = $"The ViewModel {key} does not follow the naming convention used in this project. ViewModel names should end with \"ViewModel\".";
                logger.LogInformation(message);
                throw new InvalidViewModelException(message);
            }

            if (!viewDictionary.ContainsKey(key))
            {
                var modelStringIndex = viewKey == null ? key.LastIndexOf("Model") : key.LastIndexOf($"Model_{viewKey}");
                var viewName = key.Remove(modelStringIndex);
                var message = $"The ViewModel {key} is not registered with its supposed associated View {viewName}.";
                logger.LogInformation(message);
                throw new ViewNotRegisteredException(message);
            }

            var viewType = viewDictionary[key];
            var view = Activator.CreateInstance(viewType) as Page;
            if (setStateAction != null)
            {
                viewModel.SetState(setStateAction);
                logger.LogInformation($"State has been Set for {viewModel.GetType().Name}");
            }
            if (setBindingContext)
            {
                view.BindingContext = viewModel;
                logger.LogInformation($"BindingContext has been set for {viewModel.GetType().Name}");
            }
            return view;
        }

        public Page Resolve<TViewModel>(TViewModel viewModel, bool setBindingContext = true) where TViewModel : class, IViewModel
        {
            var key = viewModel.GetType().Name;
            if (!key.EndsWith("ViewModel"))
            {
                var message = $"The ViewModel {key} does not follow the naming convention used in this project. ViewModel names should end with \"ViewModel\".";
                logger.LogInformation(message);
                throw new InvalidViewModelException(message);
            }

            if (!viewDictionary.ContainsKey(key))
            {
                var modelStringIndex = key.LastIndexOf("Model");
                var viewName = key.Remove(modelStringIndex);
                var message = $"The ViewModel {key} is not registered with its supposed associated View {viewName}.";
                logger.LogInformation(message);
                throw new ViewNotRegisteredException(message);
            }

            var viewType = viewDictionary[key];
            var view = Activator.CreateInstance(viewType) as Page;

            if (setBindingContext)
            {
                view.BindingContext = viewModel;
                logger.LogInformation($"BindingContext has been set for {viewModel.GetType().Name}.");
            }
            return view;
        }
    }
}
