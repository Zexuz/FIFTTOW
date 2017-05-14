using Android.Locations;

namespace FIFTTOW.Interfaces
{
    public interface ILocationService
    {
        Location GetLastKnowLocation();
        Location GetLocationHighAccuracy();

    }
}