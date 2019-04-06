using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using DryIoc;
using FFImageLoading.Forms.Droid;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace Weigh.Droid
{
    [Activity(Label = "@string/ApplicationName",
        Icon = "@drawable/launchscreen_icon",
        Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);
            MobileAds.Initialize(ApplicationContext, Weigh.PrivateKeys.AdmobAppId);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);


            UserDialogs.Init(this);

            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }
}

