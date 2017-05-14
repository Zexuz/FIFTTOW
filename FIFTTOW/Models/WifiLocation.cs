
using FIFTTOW.Models;

namespace FIFTTOW
{
    public class WifiLocation
    {

        public string DisplayName { get; set; }
        public WifiInfo Wifi { get; set; }
        public float Accuracy{ get; set; }
        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}