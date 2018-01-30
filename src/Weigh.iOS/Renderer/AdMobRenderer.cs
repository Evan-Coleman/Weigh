using CoreGraphics;
using Google.MobileAds;
using UIKit;
using Weigh.Controls;
using Weigh.Helpers;
using Weigh.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]

namespace Weigh.iOS.Renderers
{
    public class AdMobRenderer : ViewRenderer
    {
#if DEBUG
        private const string AdmobID = Secrets.Admob_Banner_Test_ID;
#else
        private const string AdmobID = Secrets.Admob_Banner_ID;
#endif

        private BannerView adView;
        private bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new BannerView(AdSizeCons.SmartBannerPortrait,
                    new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
                {
                    AdUnitID = AdmobID,
                    RootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController
                };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) AddSubview(adView);
                    viewOnScreen = true;
                };

                var request = Request.GetDefaultRequest();

                adView.LoadRequest(request);
                SetNativeControl(adView);
            }
        }
    }
}