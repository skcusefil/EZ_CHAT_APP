using clientXamarin.View.MainContent;
using clientXamarin.Views;
using clientXamarin.Views.MainContentView;
using Xamarin.Forms;

namespace clientXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
