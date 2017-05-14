using System;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using Autofac;
using FIFTTOW.Extenstions;
using FIFTTOW.Interfaces;
using FIFTTOW.Models;

namespace FIFTTOW.UI.Activities
{
    [Activity(Label = "WifiLocationActivity")]
    public class WifiLocationActivity : Activity
    {
        private readonly ILogService _logService;
        private readonly IStorageService<WifiLocation> _wifiLocationStorageService;

        public WifiLocationActivity()
        {
            App.Initialize(this);
            _logService = App.Container.Resolve<ILogService>();
            _wifiLocationStorageService = App.Container.Resolve<IStorageService<WifiLocation>>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WifiLocation);

            var wifiLocation = Intent.GetBundleExtra("Data").ToWifiLocation();

            // Create your application here
            var btn = FindViewById<Button>(Resource.Id.SaveButton);
            btn.Click += SaveAllData;
            FindViewById<Switch>(Resource.Id.Enable).Checked = wifiLocation.Enabled;
            FindViewById<EditText>(Resource.Id.DisplayNameValue).Text = wifiLocation.DisplayName;
            FindViewById<TextView>(Resource.Id.AccuracyValue).Text = wifiLocation.Accuracy.ToString(CultureInfo.CurrentCulture);
            FindViewById<TextView>(Resource.Id.LatValue).Text = wifiLocation.Lat.ToString(CultureInfo.CurrentCulture);
            FindViewById<TextView>(Resource.Id.LonValue).Text = wifiLocation.Lon.ToString(CultureInfo.CurrentCulture);
            FindViewById<TextView>(Resource.Id.SSIDValue).Text = wifiLocation.SSID.ToString(CultureInfo.CurrentCulture);
        }

        private void SaveAllData(object sender, EventArgs eventArgs)
        {
            var ssid = FindViewById<TextView>(Resource.Id.SSIDValue).Text;

            var wifiLocation = _wifiLocationStorageService.GetAll().Find(wifi => wifi.SSID == ssid);
            wifiLocation.Enabled = FindViewById<Switch>(Resource.Id.Enable).Checked;
            wifiLocation.DisplayName = FindViewById<EditText>(Resource.Id.DisplayNameValue).Text;

            var allOther = _wifiLocationStorageService.GetAll().Where(wifi => wifi.SSID != ssid).ToList();
            allOther.Add(wifiLocation);
            _wifiLocationStorageService.Save(allOther);
            _logService.Debug("Saving data");
            Toast.MakeText(this,"Saved!",ToastLength.Short).Show();
            Finish();
        }
    }
}