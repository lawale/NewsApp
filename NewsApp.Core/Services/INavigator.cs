using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using NewsApp.Core.ViewModel;

namespace NewsApp.Core.Services
{
    public interface INavigator
    {
        Task<IViewModel> PopAsync();
        Task<IViewModel> PopModalAsync();
        Task<TViewModel> PushAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;
        Task<TViewModel> PushAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
        Task<TViewModel> PushModalAsync<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;
        Task<TViewModel> PushModalAsync<TViewModel>(TViewModel viewModel)
            where TViewModel : class, IViewModel;
        Task PopToRootAsync();
    }
}
