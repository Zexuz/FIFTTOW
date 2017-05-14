using System.Globalization;
using Android.App;
using Android.OS;
using Android.Widget;
using FIFTTOW.Extenstions;

namespace FIFTTOW.UI.Activities
{
    [Activity(Label = "WifiLocationActivity")]
    public class WifiLocationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.WifiLocation);

            var wifiLocation = Intent.GetBundleExtra("Data").ToWifiLocation();

            // Create your application here
            FindViewById<Switch>(Resource.Id.Enable).Checked = wifiLocation.Enabled;
            FindViewById<EditText>(Resource.Id.AccuracyValue).Text = wifiLocation.Accuracy.ToString(CultureInfo.CurrentCulture);
            FindViewById<EditText>(Resource.Id.LatValue).Text = wifiLocation.Lat.ToString(CultureInfo.CurrentCulture);
            FindViewById<EditText>(Resource.Id.LonValue).Text = wifiLocation.Lon.ToString(CultureInfo.CurrentCulture);
            FindViewById<EditText>(Resource.Id.MACValue).Text = wifiLocation.Wifi.MacAddress.ToString(CultureInfo.CurrentCulture);
            FindViewById<EditText>(Resource.Id.SSIDValue).Text = wifiLocation.Wifi.Name.ToString(CultureInfo.CurrentCulture);
        }
    }
}