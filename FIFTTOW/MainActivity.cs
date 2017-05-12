using System;
using System.Collections.Generic;
using System.IO;
using Android;
using Android.App;
using Android.Content;
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

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);



//            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
//            var fileName = "WifiLocations.db";
//            var dbFullPath = Path.Combine(dbFolder, fileName);
//            try
//            {
//                using (var db = new WifiLocationContext(dbFullPath))
//                {
//                    await db.Database.MigrateAsync(); //We need to ensure the latest Migration was added. This is different than EnsureDatabaseCreated.
//
//                    var location1= new WifiLocation() { Id = 1, Name = "Home"};
//                    var location2= new WifiLocation() { Id = 2, Name = "Work"};
//                    var location3= new WifiLocation() { Id = 3, Name = "Outside"};
//
//                    var catsInTheHat = new List<WifiLocation>() { location1,location2,location3};
//
//                    if(await db.WifiLocations.CountAsync() < 3)
//                    {
//                        await db.WifiLocations.AddRangeAsync(catsInTheHat);
//                        await db.SaveChangesAsync();
//                    }
//
//                    var wifiLocations = await db.WifiLocations.ToListAsync();
//
//                    foreach(var wifiLoc in wifiLocations)
//                    {
//                        Log.Debug("DEBUG", $"{wifiLoc.Id}, {wifiLoc.Name} ");
//                    }
//                }
//
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine(ex.ToString());
//            }

//            button.Click += MyMethod;
//
//            if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Granted)
//            {
//                Console.WriteLine("asd");
//            }
//            else
//            {
//                Console.WriteLine("q654645");
//                RequestPermissions(PERMISSIONS_LOCATION,REQUEST_LOCATION);
//            }
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