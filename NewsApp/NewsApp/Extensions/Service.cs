using NewsApp.Model;
using NewsApp.View;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using NewsApp.ViewModel;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(NewsApp.Extensions.Service))]
namespace NewsApp.Extensions
{
    public class Service : IServices
    {

        public async Task<Page> NavigationPopAsync() => await Home.Detail.Navigation.PopAsync();

        public async Task NavigationPushAsync(Page page) => await Home.Detail.Navigation.PushAsync(page);

        private MasterDetailPage Home => Application.Current.MainPage as MasterDetailPage;

        public void SetDetailPage(Page page) => Home.Detail = new NavigationPage(page);

        public void SetIsPresented(bool value) => Home.IsPresented = value;

        public async Task NavigationPopToRoot() => await Home.Detail.Navigation.PopToRootAsync();
    }
}
