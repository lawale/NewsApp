using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using NewsApp.Core.Services;
using System.Threading.Tasks;

[assembly: Dependency(typeof(NavigationService))]
namespace NewsApp.Core.Services
{
    public class NavigationService : INavigationService
    {
        public Task<Page> PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }
    }
}
