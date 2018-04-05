using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Trace = System.Diagnostics.Trace;

namespace portal.PortalExInteractive.Droid
{
    [Activity(
        Theme = "@style/MainTheme",
        Label = "portal.PortalExInteractive",
        MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.User
        )]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            
            if (Device.Idiom == TargetIdiom.Phone)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            var portalFormsApp = new App
            {
                PlatformRegistrations = x =>
                {
                    x.RegisterService<ISettingsService, SettingsService>();
                    x.RegisterService<IVideoAssets, VideoAssets>();
                    x.RegisterService<IBaseUrl, DroidBaseUrl>();
                    x.RegisterService<IKeepAwakeService, KeepAwakeService>();
                    x.RegisterService<IMobileFileAccess, AndroidFileAccess>();
                    x.RegisterService<IDeviceInfoService,DeviceInfoService>();
                }
            };

            RegisterMessengerSubscriptions(portalFormsApp);

            portalFormsApp.Start();

            LoadApplication(portalFormsApp);
        }

       

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
        }
    }
}
