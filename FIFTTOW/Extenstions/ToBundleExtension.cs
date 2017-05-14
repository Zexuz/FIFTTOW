using Android.OS;
using FIFTTOW.Models;

namespace FIFTTOW.Extenstions
{
    public static class ToBundleExtension
    {
        public static Bundle ToBundle(this WifiLocation model)
        {
            var bundle = new Bundle();
            bundle.PutBoolean(nameof(model.Enabled), model.Enabled);
            bundle.PutFloat(nameof(model.Accuracy), model.Accuracy);
            bundle.PutDouble(nameof(model.Lat), model.Lat);
            bundle.PutDouble(nameof(model.Lon), model.Lon);
            bundle.PutString(nameof(model.DisplayName), model.DisplayName);
            bundle.PutString(nameof(model.SSID), model.SSID);
            return bundle;
        }

        public static WifiLocation ToWifiLocation(this Bundle model)
        {
            var wifiLocation = new WifiLocation();

            wifiLocation.Accuracy = model.GetFloat(nameof(wifiLocation.Accuracy));
            wifiLocation.DisplayName= model.GetString(nameof(wifiLocation.DisplayName));
            wifiLocation.Enabled= model.GetBoolean(nameof(wifiLocation.Enabled));
            wifiLocation.Lat= model.GetDouble(nameof(wifiLocation.Lat));
            wifiLocation.Lon= model.GetDouble(nameof(wifiLocation.Lon));
            wifiLocation.SSID= model.GetString(nameof(wifiLocation.SSID));

            return wifiLocation;
        }
    }
}