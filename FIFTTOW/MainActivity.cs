using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using Android.OS;
using Android.Util;
using FIFTTOW.Servicies;
using Android.Locations;
using Android.Views;


namespace FIFTTOW
{
    [Activity(Label = "FIFTTOW", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        static readonly int REQUEST_LOCATION = 1;

        static string[] PERMISSIONS_LOCATION = {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.LocationHardware,
            Manifest.Permission.AccessCoarseLocation,
        };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);


            button.Click += MyMethod;

            if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                Console.WriteLine("asd");
            }
            else
            {
                Console.WriteLine("q654645");
                RequestPermissions(PERMISSIONS_LOCATION,REQUEST_LOCATION);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if(requestCode == REQUEST_LOCATION)
            {
                Console.WriteLine("test");
            }
        }


        private void MyMethod(object sender, EventArgs e)
        {
            var wifiService = new WifiService();

            var isConnected = wifiService.IsConnectedToWifi(this);
            Log.Debug("DEBUG", $"Is connected {isConnected}");
            Toast.MakeText(this, $"Is connected {isConnected}", ToastLength.Long).Show();

            var criteria = new Criteria
            {
                Accuracy = Accuracy.Coarse
            };

            var locationManager = (LocationManager) GetSystemService(LocationService);
            var provider = locationManager.GetBestProvider(criteria, false);
            using (var loc = locationManager.GetLastKnownLocation(provider))
            {
                Log.Debug("DEBUG", $"Acc {loc.Accuracy}, lat {loc.Latitude}, lon {loc.Longitude}");
                Console.WriteLine(loc);
            }

        }

    }


}