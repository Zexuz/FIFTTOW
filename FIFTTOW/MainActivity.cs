using System;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using FIFTTOW.Servicies;
using Android.Locations;
using Autofac;
using FIFTTOW.Interfaces;

namespace FIFTTOW
{
    [Activity(Label = "FIFTTOW", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public  IPermissionsService PermissionsService;
        public  ILogService LogService;
        public IStorageService<WifiLocation> WifiLocationStorageService;
        public IWifiService WifiService;

        public MainActivity()
        {
            App.Initialize(this);
            LogService = App.Container.Resolve<ILogService>();
            WifiService= App.Container.Resolve<IWifiService>();
            PermissionsService = App.Container.Resolve<IPermissionsService>();
            WifiLocationStorageService= App.Container.Resolve<IStorageService<WifiLocation>>();

        }

//        public MainActivity(WifiService wifiService, StorageService<WifiLocation> wifiLocationStorageService, ILogService logService, PermissionsService permissionsService)
//        {
//            _wifiService = wifiService;
//            _wifiLocationStorageService = wifiLocationStorageService;
//            _logService = logService;
//            _permissionsService = permissionsService;
//        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
//            _permissionsService = new PermissionsService();
//            _logService = new DebugLogService(this);
//            _wifiLocationStorageService = new StorageService<WifiLocation>();
//            _wifiService = new WifiService();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += SaveWifiLocation;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsService.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void SaveWifiLocation(object sender, EventArgs e)
        {

            var isConnected = WifiService.IsConnectedToWifi();
            LogService.Debug($"Is connected {isConnected}");

            var criteria = new Criteria
            {
                Accuracy = Accuracy.Coarse
            };

            var locationManager = (LocationManager) GetSystemService(LocationService);
            var provider = locationManager.GetBestProvider(criteria, false);
            using (var loc = locationManager.GetLastKnownLocation(provider))
            {
                LogService.Debug($"Acc {loc.Accuracy}, lat {loc.Latitude}, lon {loc.Longitude}");
            }
        }
    }
}