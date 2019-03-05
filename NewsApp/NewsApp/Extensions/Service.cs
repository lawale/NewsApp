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
        private HomePage Home;
        //private NavigationPage NavigationPage;
        public Service(ref HomePage home, ref NavigationPage navigationPage)
        {
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
        }

        public void SetIsPresented(bool value)
        {
            Home.IsPresented = value;
        }

        public async Task NavigationPopToRoot()
        {
            await MainPage.Detail.Navigation.PopToRootAsync();
        }
    }
}
