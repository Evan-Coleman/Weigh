using CoreGraphics;
using Google.MobileAds;
using System;
using UIKit;
using Weigh.Controls;
using Weigh.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]

namespace Weigh.iOS.Renderers
{
    public class AdMobRenderer : ViewRenderer
    {
        const string AdmobID = "_your_admob_ad_unit_id_goes_here_";

        BannerView adView;
        bool viewOnScreen;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            if (e.OldElement == null)
            {
                adView = new BannerView(size: AdSizeCons.SmartBannerPortrait,
                                           origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
                {
                    AdUnitID = AdmobID,
                    RootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController
            };

                adView.AdReceived += (sender, args) =>
                {
                    if (!viewOnScreen) this.AddSubview(adView);
                    viewOnScreen = true;
                };

                var request = Request.GetDefaultRequest();

                adView.LoadRequest(request);
                base.SetNativeControl(adView);
            }
        }
    }
}