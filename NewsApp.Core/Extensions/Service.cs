using NewsApp.Core.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NewsApp.Core.Extensions.Service))]
namespace NewsApp.Core.Extensions
{
    public class Service : IServices
    {

        public Page CurrentPage { get; set; }

        readonly INavigationImpl Navigation = DependencyService.Get<INavigationImpl>();

        public async Task<Page> NavigationPopAsync() => await Home.Detail.Navigation.PopAsync();

        public async Task NavigationPushAsync(Page page) => await Home.Detail.Navigation.PushAsync(page);

        private MasterDetailPage Home => Application.Current.MainPage as MasterDetailPage;

        public void SetDetailPage(Page page)
        {
            Navigation.ChangeDetail(page);
        }

        public void SetIsPresented(bool value) => Home.IsPresented = value;

        public async Task NavigationPopToRoot() => await Home.Detail.Navigation.PopToRootAsync();
    }
}