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
using FIFTTOW.Exceptions;
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
        private readonly ILocationService _locationService;

        //UI
        private Button _addLocationButton;

        //Misc
        private List<WifiLocation> _locations;

        public MainActivity()
        {
            App.Initialize(this);
            _logService = App.Container.Resolve<ILogService>();
            _wifiService = App.Container.Resolve<IWifiService>();
            _permissionsService = App.Container.Resolve<IPermissionsService>();
            _wifiLocationStorageService = App.Container.Resolve<IStorageService<WifiLocation>>();
            _locationService = App.Container.Resolve<ILocationService>();

            _locations = _wifiLocationStorageService.GetAll();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _locations = _wifiLocationStorageService.GetAll();
            ListAdapter = new WifiLocationAdapter(this, _locations);
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
            var wifiLocation = ((WifiLocationAdapter) ListAdapter)[position];
//            Toast.MakeText(this, wifiLocation.DisplayName, ToastLength.Short).Show();

            var intent = new Intent(this, typeof(WifiLocationActivity));
            intent.PutExtra("Data", wifiLocation.ToBundle());
            StartActivity(intent);
        }


        private void SaveWifiLocation(object sender, EventArgs e)
        {

            _locationService.GetLocationHighAccuracy();
//            using (var loc = _locationService.GetLastKnowLocation())
//            {
//                var wifiLoc = new WifiLocation()
//                {
//                    SSID = _wifiService.GetWifiISSID(),
//                    Accuracy = loc.Accuracy,
//                    DisplayName = _wifiService.GetWifiISSID(),
//                    Lat = loc.Latitude,
//                    Lon = loc.Longitude,
//                    Enabled = true
//                };
//                _locations.Add(wifiLoc);
//                try
//                {
//                    _wifiLocationStorageService.Add(wifiLoc);
//                }
//                catch (ItemAlredySavedException exception)
//                {
//                    Toast.MakeText(this, exception.Message, ToastLength.Long).Show();
//                    return;
//                }
//            }
//
//            UpdateUi();
        }

        private void UpdateUi()
        {
            RunOnUiThread(() => { ((WifiLocationAdapter) ListAdapter).NotifyDataSetChanged(); });
        }
    }
}