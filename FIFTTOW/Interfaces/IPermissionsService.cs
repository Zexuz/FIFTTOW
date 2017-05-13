using Android.App;
using Android.Content.PM;

namespace FIFTTOW.Interfaces
{
    public interface IPermissionsService
    {
        void CheckAndAskForPermission(Activity inActivity);
        void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults);
    }
}