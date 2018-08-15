using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NewsApp.Extensions
{
    public class Service : IServices
    {
        public async Task<Page> NavigationPopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        public async Task NavigationPushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        private Page MainPage
        {
            get
            {
                return Application.Current.MainPage;
            }
        }
    }
}
