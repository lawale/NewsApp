using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using NewsApp.Core.Services;
using NewsApp.Core.Extensions;

[assembly: Dependency(typeof(NavigationImpl))]
namespace NewsApp.Core.Services
{
    public class NavigationImpl : NavigationProxy, INavigationImpl
    {
        public Page CurrentDetail { get; private set; }

        public MasterDetailSetting MasterDetailSetting { get; private set; }

        Page OldPage
        {
            get => DependencyService.Get<IServices>().CurrentPage;
            set => DependencyService.Get<IServices>().CurrentPage = value;
        }

        public void SetDetail(Page page)
        {
            if(Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                if(masterDetailPage.Detail is NavigationPage navigationPage)
                {
                    navigationPage.Navigation.InsertPageBefore(page, OldPage);
                    navigationPage.Navigation.PopAsync();
                    switch(MasterDetailSetting)
                    {
                        case MasterDetailSetting.CloseMasterAfterNavigation:
                            masterDetailPage.IsPresented = false;
                            break;
                        case MasterDetailSetting.LeaveMasterOpenAfterNavigation:
                            masterDetailPage.IsPresented = true;
                            break;
                    }
                    CurrentDetail = page;
                    OldPage = page;
                }
            }
        }

        public void SetMasterDetail(MasterDetailPage masterDetailPage, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation)
        {
            if(string.IsNullOrWhiteSpace(masterDetailPage.Master.Title))
            {
                masterDetailPage.Master.Title = "Menu";
            }
            if(masterDetailPage.Detail.GetType() !=  typeof(NavigationPage))
            {
                Type detail = masterDetailPage.Detail.GetType();
                var newDetail = new NavigationPage(Activator.CreateInstance(detail) as Page);
                masterDetailPage.Detail = newDetail;
            }
            var currDetail = masterDetailPage.Detail as NavigationPage;
            CurrentDetail = currDetail.RootPage;
            Application.Current.MainPage = masterDetailPage;
        }

        public void SetMasterDetail(Page masterPage, Page detailPage, MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation)
        {
            var masterDetailPage = new MasterDetailPage
            {
                Master = masterPage,
                Detail = detailPage,
                MasterBehavior = masterBehavior
            };
            SetMasterDetail(masterDetailPage, masterDetailSetting);
        }
    }
}
