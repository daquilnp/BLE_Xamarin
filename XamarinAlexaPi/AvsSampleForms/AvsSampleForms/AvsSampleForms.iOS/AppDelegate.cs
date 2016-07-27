using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace AvsSampleForms.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();

            global::ImageCircle.Forms.Plugin.iOS.ImageCircleRenderer.Init ();

            LoadApplication (new App ());

            return base.FinishedLaunching (app, options);
        }

        public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // Pass on the url to the SDK to parse authorization code from the url.
            bool isValidRedirectSignInURL = Amazon.Login.Mobile.HandleOpenUrl (url, sourceApplication);
            if(!isValidRedirectSignInURL)
                return false;

            // App may also want to handle url 
            return true;
        }
    }
}

