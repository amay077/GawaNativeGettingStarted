using System;
using Android.Content;
using GawaNativeGettingStarted.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebView), typeof(GNWebViewRenderer))]
namespace GawaNativeGettingStarted.Droid
{
    class GNWebViewRenderer : WebViewRenderer
    {
        public GNWebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;

            Control.ClearCache(true);
            Control.Settings.SetAppCacheEnabled(false);
            Control.Settings.CacheMode = Android.Webkit.CacheModes.NoCache;
        }
    }
}
