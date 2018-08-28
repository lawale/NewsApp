using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using NewsApp.Model;
using NewsApp.ViewModel;

namespace NewsApp.Extensions
{
    public interface IServices
    {
        List<LinkViewModel> Bookmarks();
        void BookmarkLink(LinkViewModel model);
        bool GetBookmarks(LinkViewModel url);
        void RemoveBookmark(LinkViewModel url);
        Task NavigationPopToRoot();
        Task<Page> NavigationPopAsync();
        Task NavigationPushAsync(Page page);
        void SetDetailPage(Page page);
        void SetIsPresented(bool value);
    }
}