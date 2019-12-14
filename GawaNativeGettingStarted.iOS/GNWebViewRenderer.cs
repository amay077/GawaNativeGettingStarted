using System;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using WebViewSample.iOS;

[assembly: ExportRenderer(typeof(WebView), typeof(GNWebViewRenderer))]
namespace WebViewSample.iOS
{
    class GNWebViewRenderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (Element == null) return;

            NSUrlCache.SharedCache.RemoveAllCachedResponses();
            NSUrlCache.SharedCache.MemoryCapacity = 0;
            NSUrlCache.SharedCache.DiskCapacity = 0;
        }
    }
}
