using System.Threading.Tasks;
using NewsApp.Core.ViewModel;

namespace NewsApp.Core.Services
{
    public interface INavigationService
    {
        Task<object> ModalNavigateBackward();
        Task ModalNavigateForward<TViewModel>(bool setBindingContext = true) where TViewModel : class, IViewModel;
        Task ModalNavigateForward<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel;
        Task<object> NavigateBackward();
        Task NavigateBackwardToRoot();
        Task NavigateForward<TViewModel>(bool setBindingContext = true) where TViewModel : class, IViewModel;
        Task NavigateForward<TViewModel>(TViewModel viewModel) where TViewModel : class, IViewModel;
    }
}