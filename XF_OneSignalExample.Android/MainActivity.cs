using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System.Collections.Generic;

namespace XF_OneSignalExample.Droid
{
    [Activity(Label = "XF_OneSignalExample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            OneSignal.Current.StartInit("Your OneSignal token")
                .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
                 .HandleNotificationReceived(HandleNotificationReceived)
                 .HandleNotificationOpened(HandleNotificationOpened)
                 .EndInit();


            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            var a = prefs.GetString("is_notification_received", "false");

            if (a == "true")
            {
                LoadApplication(new App(true));

            }
            else
            {
                LoadApplication(new App(false));

            }
            OneSignal.Current.IdsAvailable(IdsAvaible);
        }

        private async void IdsAvaible(string userId, string pushToken)
        {
            //you can save push notification identifier to local storage in here 
        }

        // Called when your app is in focus and a notificaiton is recieved.
        private static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;
        }

        private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            //Handle when notification clicked
            OSNotificationPayload payload = result.notification.payload;
            Dictionary<string, object> additionalData = payload.additionalData;

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Android.App.Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("is_notification_received", "true");
            editor.Apply();
        }

        protected override void OnResume()
        {
            base.OnResume();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Remove("is_notification_received");
            editor.PutString("is_notification_received", "false");
            editor.Apply();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
    }
}