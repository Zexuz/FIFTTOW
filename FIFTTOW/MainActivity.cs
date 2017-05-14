using System;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Autofac;
using FIFTTOW.Interfaces;

namespace FIFTTOW
{
    [Activity(Label = "FIFTTOW", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private readonly IPermissionsService _permissionsService;
        private readonly ILogService _logService;
        private readonly IStorageService<WifiLocation> _wifiLocationStorageService;
        private readonly IWifiService _wifiService;

        public MainActivity()
        {
            App.Initialize(this);
            _logService = App.Container.Resolve<ILogService>();
            _wifiService= App.Container.Resolve<IWifiService>();
            _permissionsService = App.Container.Resolve<IPermissionsService>();
            _wifiLocationStorageService= App.Container.Resolve<IStorageService<WifiLocation>>();
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            _permissionsService.CheckAndAskForPermission(this);
            var button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += SaveWifiLocation;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            _permissionsService.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void SaveWifiLocation(object sender, EventArgs e)
        {

            var isConnected = _wifiService.IsConnectedToWifi();
            _logService.Debug($"Is connected {isConnected}");

            var criteria = new Criteria
            {
                Accuracy = Accuracy.NoRequirement
            };

            var locationManager = (LocationManager) GetSystemService(LocationService);
            var provider = locationManager.GetBestProvider(criteria, false);
            using (var loc = locationManager.GetLastKnownLocation(provider))
            {
                var wifiLoc = new WifiLocation()
                {
                    Wifi = _wifiService.GetWifiInfo(),
                    Accuracy = loc.Accuracy,
                    DisplayName = _wifiService.GetWifiInfo().Name,
                    Lat = loc.Latitude,
                    Lon = loc.Longitude

                };
                _wifiLocationStorageService.Add(wifiLoc);
            }
        }
    }
}