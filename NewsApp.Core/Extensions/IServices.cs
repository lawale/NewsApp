using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using NewsApp.Core.ViewModel;

namespace NewsApp.Core.Extensions
{
    public interface IServices
    {
        Page CurrentPage { get; set; }
        Task NavigationPopToRoot();
        Task<Page> NavigationPopAsync();
        Task NavigationPushAsync(Page page);
        void SetDetailPage(Page page);
        void SetIsPresented(bool value);
    }
}