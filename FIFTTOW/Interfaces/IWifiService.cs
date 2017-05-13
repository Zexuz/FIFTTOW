using FIFTTOW.Servicies;

namespace FIFTTOW.Interfaces
{
    public interface IWifiService
    {
        bool IsConnectedToWifi();
        WifiInfo GetWifiInfo();
        void ConnectToWifi(WifiInfo wifiInfo);
        void DisableWifi();
        void ActivateWifi();
        bool HasWifiEnabled();
    }
}