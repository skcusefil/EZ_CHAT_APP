using clientXamarin.Interfaces;
using clientXamarin.Services;
using clientXamarin.ViewModels.MainContentViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clientXamarin.Views.MainContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        IMemberService _member = new MemberService();

        public ProfilePage()
        {
            InitializeComponent();

            var page = new NavigationService();
            BindingContext = new ProfileViewModel(page);
        }

        protected async override void OnAppearing()
        {
            var userInfo = await _member.GetMember(Preferences.Get("username",""));

            displayName.Text = userInfo.DisplayName;
            //profileImage.Source = userInfo.PhotoUrl;

            base.OnAppearing();
        }
    }
}