using clientXamarin.Services;
using clientXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace clientXamarin
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var page = new NavigationService();

            BindingContext = new LoginViewModel(page);
        }

    }
}
