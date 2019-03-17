using NewsApp.Core.Extensions;
using NewsApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NewsApp.Core.Services
{
    public interface INavigationImpl : INavigation
    {
        void SetDetail(Page page);

        Page CurrentDetail { get; }

        MasterDetailSetting MasterDetailSetting { get; }

        void SetMasterDetail(MasterDetailPage masterDetailPage, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation);

        void SetMasterDetail(Page masterPage, Page detailPage, MasterBehavior masterBehavior = MasterBehavior.Default, MasterDetailSetting masterDetailSetting = MasterDetailSetting.CloseMasterAfterNavigation);
    }
}
