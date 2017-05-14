using FIFTTOW.Models;

namespace FIFTTOW.Interfaces
{
    public interface IWifiService
    {
        bool IsConnectedToWifi();
        string GetWifiISSID();
        void ConnectToWifi(string SSID);
        void DisableWifi();
        void ActivateWifi();
        bool HasWifiEnabled();
    }
}