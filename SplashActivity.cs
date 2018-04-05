using Android.App;
using Android.OS;

namespace portal.PortalExInteractive.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, Icon = "@drawable/ic_launcher", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.Insights.Initialize(XamarinInsights.ApiKey, this);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Splash);
        }

        protected override void OnStart()
        {
            base.OnStart();
            StartActivity(typeof(MainActivity));
        }
    }
}
