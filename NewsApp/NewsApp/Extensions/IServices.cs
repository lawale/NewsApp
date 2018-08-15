using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsApp.Extensions
{
    public interface IServices
    {
        Task<Page> NavigationPopAsync();
        Task NavigationPushAsync(Page page);
    }
}
