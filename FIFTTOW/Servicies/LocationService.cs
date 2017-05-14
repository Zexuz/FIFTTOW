using System;
using Android.Content;
using Android.Locations;
using Android.OS;
using Autofac;
using FIFTTOW.Interfaces;

namespace FIFTTOW.Servicies
{
    public class LocationService : ILocationService, ILocationListener
    {
        private readonly IWifiService _wifiService;
        private readonly ILogService _logService;
        private readonly Context _context;

        public LocationService(Context context,IWifiService wifiService, ILogService logService)
        {
            _wifiService = wifiService;
            _logService = logService;
//            App.Initialize(context);
//            _logService = App.Container.Resolve<ILogService>();
//            _wifiService = App.Container.Resolve<IWifiService>();
            _context = context;
        }

        public Location GetLastKnowLocation()
        {
            var criteria = new Criteria
            {
                Accuracy = Accuracy.NoRequirement
            };

            var locationManager = GetLocationManager();

            var provider = locationManager.GetBestProvider(criteria, false);
            return locationManager.GetLastKnownLocation(provider);
        }

        public Location GetLocationHighAccuracy()
        {
//            var locationManager = GetLocationManager();
//
//            var lastGPS = locationManager.GetLastKnownLocation(LocationManager.GpsProvider);
//            var lastNetwork = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
//            var lastPassive = locationManager.GetLastKnownLocation(LocationManager.PassiveProvider);
//
//            var criteria = new Criteria
//            {
//                Accuracy = Accuracy.Fine
//            };
//
//            locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 10000, 1, this);
//            locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 10000, 1, this);
//            locationManager.RequestLocationUpdates(LocationManager.PassiveProvider, 10000, 1, this);

            return null;
        }

        private LocationManager GetLocationManager()
        {
            return (LocationManager) _context.GetSystemService(Context.LocationService);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IntPtr Handle { get; }
        public void OnLocationChanged(Location location)
        {
            _logService.Debug(nameof(OnLocationChanged));
        }

        public void OnProviderDisabled(string provider)
        {
            _logService.Debug(nameof(OnProviderDisabled));

        }

        public void OnProviderEnabled(string provider)
        {
            _logService.Debug(nameof(OnProviderEnabled));
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            _logService.Debug(nameof(OnStatusChanged));

        }
    }
}