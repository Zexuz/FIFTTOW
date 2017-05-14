using Android.Content;
using Autofac;
using FIFTTOW.Interfaces;
using FIFTTOW.Models;
using FIFTTOW.Servicies;

namespace FIFTTOW
{
    public class App
    {
        public static IContainer Container { get; set; }

        public static void Initialize(Context context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DebugLogService>().As<ILogService>();

            builder.RegisterType<WifiService>().As<IWifiService>();

            builder.RegisterType<PermissionsService>().As<IPermissionsService>();

            builder.RegisterType<WifiLocationStorageService>().As<IStorageService<WifiLocation>>();

            builder.RegisterType<LocationService>().As<ILocationService>();

            builder.Register(ctx => context).As(typeof(Context));

            Container = builder.Build();
        }
    }
}