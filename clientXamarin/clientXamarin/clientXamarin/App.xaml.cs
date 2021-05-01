using clientXamarin.Helpers;
using clientXamarin.View.MainContent;
using clientXamarin.Views;
using clientXamarin.Views.MainContentView;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var accessToken = Preferences.Get("accessToken", "");
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new ProfilePage());

            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    //dont have any access token yet will navigate to Login Page
            //    MainPage = new NavigationPage(new MainPage());

            //}
            //else
            //{
            //    // already login and get access token can go direkt to AppShell
            //    MainPage = new AppShell();
            //}
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            //Handle connection when app starts 
            //using Analytics need to install nugetpackage Microsoft.AppCenter.Analytics;
            //using Crashes need to install nugetpackage Microsoft.AppCenter.Crashes;
            //using Distribute need to install nugetpackage Microsoft.AppCenter.Distribute;

            if (DeviceInfo.Platform == DevicePlatform.Android && ServerConnectionSetting.AppCenterAndroid != "AC_ANDROID")
            {
                AppCenter.Start($"android={ServerConnectionSetting.AppCenterAndroid};" +
                    "uwp={Your UWP App secret here};" +
                    "ios={Your iOS App secret here}",
                    typeof(Analytics), typeof(Crashes), typeof(Distribute));
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
