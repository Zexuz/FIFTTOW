using Android.Content;
using Android.Net;
using FIFTTOW.Interfaces;
using Android.Net;
using Android.Net.Wifi;

namespace FIFTTOW.Servicies
{
    public class WifiService
    {
        public bool IsConnectedToWifi(Context ctx)
        {
            var wifiManager = (WifiManager) ctx.GetSystemService(Context.WifiService);
            return wifiManager.IsWifiEnabled;
        }
    }
}