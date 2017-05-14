using System.Linq;
using Android.Content;
using FIFTTOW.Interfaces;
using Android.Net.Wifi;
using FIFTTOW.Exceptions;

namespace FIFTTOW.Servicies
{

    public class WifiService : IWifiService
    {
        private readonly Context _context;

        public WifiService(Context context)
        {
            _context = context;
        }


        public bool IsConnectedToWifi()
        {
            var wifiManager = GetWifiManager();
            return wifiManager.ConnectionInfo.SupplicantState == SupplicantState.Completed;
        }


        public string GetWifiISSID()
        {
            var wifiManager = GetWifiManager();
            return wifiManager.ConnectionInfo.SSID;
        }

        public void ConnectToWifi(string SSID)
        {
            ActivateWifi();
            var wifiManager = GetWifiManager();
            var conf = wifiManager.ConfiguredNetworks.SingleOrDefault(wifi => wifi.Ssid == SSID);
            if(conf == null)
            {
                throw new WifiNetworkNotFoundException($"The Wifi network with name '{SSID}' can't be found in stored Wifi connections");
            }
            wifiManager.EnableNetwork(conf.NetworkId, true);
        }

        public void DisableWifi()
        {
            var wifiManager = GetWifiManager();
            wifiManager.SetWifiEnabled(false);
        }

        public void ActivateWifi()
        {
            var wifiManager = GetWifiManager();
            wifiManager.SetWifiEnabled(true);
        }


        public bool HasWifiEnabled()
        {
            var wifiManager = GetWifiManager();
            return wifiManager.IsWifiEnabled;
        }

        private WifiManager GetWifiManager()
        {
            return (WifiManager) _context.GetSystemService(Context.WifiService);
        }
    }
}