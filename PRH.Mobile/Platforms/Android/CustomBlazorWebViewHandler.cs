using Android.Webkit;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace PRH.Mobile.Platforms.Android
{
    public class CustomWebChromeClient : WebChromeClient
    {
        public override void OnPermissionRequest(PermissionRequest request)
            => request.Grant(request.GetResources());
    }

    public class CustomBlazorWebViewHandler : BlazorWebViewHandler
    {
        protected override global::Android.Webkit.WebView CreatePlatformView()
        {
            var webView = base.CreatePlatformView();
            webView.SetWebChromeClient(new CustomWebChromeClient());
            return webView;
        }

        protected override void ConnectHandler(global::Android.Webkit.WebView platformView)
        {
            base.ConnectHandler(platformView);
            platformView.Settings.JavaScriptEnabled = true;
            platformView.Settings.AllowFileAccess = true;
            platformView.Settings.AllowFileAccessFromFileURLs = true;
            platformView.Settings.AllowUniversalAccessFromFileURLs = true;
        }
    }
}