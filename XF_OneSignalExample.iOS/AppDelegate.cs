using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XF_OneSignalExample.Pages;

namespace XF_OneSignalExample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            OneSignal.Current.StartInit("Your OneSignal token")
                .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
               .HandleNotificationOpened(HandleNotificationOpened)
               .HandleNotificationReceived(HandleNotificationReceived)
                 .EndInit();
            LoadApplication(new App(false));
            OneSignal.Current.IdsAvailable(IdsAvaible);

            return base.FinishedLaunching(app, options);
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

        //if notification clicked, open directly notification list page
        private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            //Handle when notification clicked
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#0B2E5D"),
                BarTextColor = Color.White,
            };

            Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotificationListPage());

        }
    }
}
