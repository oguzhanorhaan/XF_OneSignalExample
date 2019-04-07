using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF_OneSignalExample.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XF_OneSignalExample
{
    public partial class App : Application
    {
        public App(bool isNotificationReceived)
        {
            InitializeComponent();

            CheckStartPage(isNotificationReceived);
        }

        private void CheckStartPage(bool isNotificationReceived)
        {
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#0B2E5D"),
                BarTextColor = Color.White,
            };

            //if notification clicked, open directly notification list page
            if (isNotificationReceived)
            {
                Application.Current.MainPage.Navigation.PushAsync(new NotificationListPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
