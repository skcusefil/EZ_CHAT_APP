using clientXamarin.Views.MainContentView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clientXamarin.View.MainContent
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();

            tabBar.CurrentItem = chatMainPage;

            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes( )
        {
            //var JsonStr = JsonConvert.SerializeObject();

            Routes.Add(nameof(ChatMainPage), typeof(ChatMainPage));
            Routes.Add(nameof(ProfilePage), typeof(ProfilePage));

        }
    }
}