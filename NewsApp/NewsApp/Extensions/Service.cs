using NewsApp.Model;
using NewsApp.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using NewsApp.ViewModel;
using System.Collections.ObjectModel;

namespace NewsApp.Extensions
{
    public class Service : IServices
    {
        private List<LinkViewModel> links;
        private HomePage Home;
        //private NavigationPage NavigationPage;
        public List<LinkViewModel> Bookmarks()
        {
            return links;
        }
        public Service(ref HomePage home, ref NavigationPage navigationPage)
        {
            links = new List<LinkViewModel>();
            Home = home;
            //NavigationPage = navigationPage;
        }
        public async Task<Page> NavigationPopAsync()
        {
            return await MainPage.Detail.Navigation.PopAsync();
        }

        public async Task NavigationPushAsync(Page page)
        {
            await MainPage.Detail.Navigation.PushAsync(page);
        }

        private MasterDetailPage MainPage
        {
            get
            {
                return Application.Current.MainPage as MasterDetailPage; 
            }
        }

        public void SetDetailPage(Page page)
        {
            Home.Detail = new NavigationPage(page);
            GC.Collect(0);
        }

        public void SetIsPresented(bool value)
        {
            Home.IsPresented = value;
        }

        public async Task NavigationPopToRoot()
        {
            await MainPage.Detail.Navigation.PopToRootAsync();
        }

        public bool GetBookmarks(LinkViewModel url)
        {
            var exists = links.Contains(url);
            return exists;
        }

        public void BookmarkLink(LinkViewModel model)
        {
            links.Add(model);
        }

        public void RemoveBookmark(LinkViewModel url)
        {
            if (GetBookmarks(url))
                links.Remove(url);
            else
                return;
        }
    }
}
