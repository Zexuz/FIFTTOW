using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using Autofac;
using FIFTTOW.Extenstions;
using FIFTTOW.Interfaces;
using FIFTTOW.Models;

namespace FIFTTOW.UI.Activities
{
    [Activity(Label = "FIFTTOW", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        //DI
        private readonly IPermissionsService _permissionsService;

        private readonly ILogService _logService;
        private readonly IStorageService<WifiLocation> _wifiLocationStorageService;
        private readonly IWifiService _wifiService;

        //UI
        private Button _addLocationButton;

        //Misc
        private readonly List<WifiLocation> _locations;

        public MainActivity()
        {
            App.Initialize(this);
            _logService = App.Container.Resolve<ILogService>();
            _wifiService = App.Container.Resolve<IWifiService>();
            _permissionsService = App.Container.Resolve<IPermissionsService>();
            _wifiLocationStorageService = App.Container.Resolve<IStorageService<WifiLocation>>();
            _locations = _wifiLocationStorageService.GetAll();

        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            ListAdapter = new WifiLocationAdapter(this, _locations);

            _permissionsService.CheckAndAskForPermission(this);

            _addLocationButton = FindViewById<Button>(Resource.Id.MyButton);
            _addLocationButton.Click += SaveWifiLocation;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            _permissionsService.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var wifiLocation = ((WifiLocationAdapter)ListAdapter)[position];
//            Toast.MakeText(this, wifiLocation.DisplayName, ToastLength.Short).Show();

            var intent = new Intent(this, typeof(WifiLocationActivity));
            intent.PutExtra("Data", wifiLocation.ToBundle());
            StartActivity(intent);
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
                    DisplayName = _wifiService.GetWifiInfo().Name + _locations.Count,
                    Lat = loc.Latitude,
                    Lon = loc.Longitude,
                    Enabled =  true
                };
                _wifiLocationStorageService.Add(wifiLoc);
                _locations.Add(wifiLoc);
            }

            UpdateUi();
        }

        private void UpdateUi()
        {
            RunOnUiThread(() =>
            {
                ((WifiLocationAdapter) ListAdapter).NotifyDataSetChanged();
            });
        }
    }
}