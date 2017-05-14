
using Android.OS;

namespace FIFTTOW.Models
{
    public class WifiLocation
    {

        public string DisplayName { get; set; }
        public string SSID { get; set; }
        public float Accuracy{ get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
        public bool Enabled { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}