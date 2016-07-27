using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.RecordAudio)]

namespace AvsSampleForms.Droid
{
    [Activity (Label = "AVS Sample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            ActionBar.SetIcon (Android.Resource.Color.Transparent);

            global::Xamarin.Forms.Forms.Init (this, bundle);

            global::Xamarin.Avs.AlexaVoiceService.Init (this);


            ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init ();

            LoadApplication (new App ());
        }
    }
}

