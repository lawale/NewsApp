using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsApp.Core.Services
{
    public interface INavigationService 
    {
        Task<Page> PopAsync();
        Task<Page> PopModalAsync();
        Task PopToRootAsync();
        Task PushAsync(Page page);
        Task PushModalAsync(Page page);
    }
}
