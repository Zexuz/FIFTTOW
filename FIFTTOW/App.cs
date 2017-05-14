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
            builder.Register(ctx => new DebugLogService(context)).As<ILogService>();

            builder.RegisterType<WifiService>().As<IWifiService>();
            builder.Register(ctx => new WifiService(context)).As<IWifiService>();

            builder.RegisterType<PermissionsService>().As<IPermissionsService>();
            builder.RegisterType<WifiLocationStorageService>().As<IStorageService<WifiLocation>>();

            Container = builder.Build();
        }
    }
}