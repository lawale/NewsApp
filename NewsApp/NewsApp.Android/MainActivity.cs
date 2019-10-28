using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Android.Content;
using FFImageLoading.Forms.Platform;
using XF.Material.Droid;

namespace NewsApp.Droid
{
    [Activity(Label = "News Feed", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            PackageInit(bundle);
            
            LoadApplication(new App());
        }

        /// <summary>
        /// Init Packages in this method
        /// </summary>
        void PackageInit(Bundle bundle)
        {
            Forms.Init(this, bundle);
            Material.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            CachedImageRenderer.Init(true);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}

