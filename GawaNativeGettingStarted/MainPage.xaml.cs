using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace GawaNativeGettingStarted
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            webView.Navigated += (s, e) =>
            {
                progressView.IsVisible = false;
                webView.IsVisible = true;
            };

        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            // 許可を求める権限群
            var permissions = new []
            {
                Permission.Location,
                Permission.Camera,
            };

            var notPermitteds = new List<Permission>();
            foreach (var perm in permissions)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(perm);
                if (status != PermissionStatus.Granted)
                {
                   notPermitteds.Add(perm);
                }
            }

            var statusMap = await CrossPermissions.Current.RequestPermissionsAsync(notPermitteds.ToArray());
            var permitted = statusMap.Values.All(x => x == PermissionStatus.Granted);

            if (!permitted)
            {
                await DisplayAlert("許可求む", "すべての権限が許可されていないので、一部の機能が使用できないかもしれません。", "閉じる");
            }

            // 許可されたら Web アプリ起動
            webView.Source = "https://5ce07f72.ngrok.io";
        }
    }
}
