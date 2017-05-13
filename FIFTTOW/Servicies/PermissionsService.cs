using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using FIFTTOW.Interfaces;

namespace FIFTTOW.Servicies
{
    public class PermissionsService : IPermissionsService
    {
        private static readonly int REQUEST_LOCATION = 1;

        private static string[] PERMISSIONS_LOCATION =
        {
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.LocationHardware,
            Manifest.Permission.AccessCoarseLocation,
        };

        public void CheckAndAskForPermission(Activity inActivity)
        {
            if (inActivity.BaseContext.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == Permission.Denied)
            {
                inActivity.RequestPermissions(PERMISSIONS_LOCATION, REQUEST_LOCATION);
            }
        }

        public void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {

        }
    }
}