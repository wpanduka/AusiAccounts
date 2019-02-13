using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;
using Plugin.CurrentActivity;
using Plugin.Media;

namespace ADB2CAuthorization.Droid
{
    [Activity(Label = "Ausi Account App", Icon = "@drawable/appicon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            await CrossMedia.Current.Initialize();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            CrossCurrentActivity.Current.Init(this, bundle);
            App.AuthenticationClient.PlatformParameters = new PlatformParameters(this);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

