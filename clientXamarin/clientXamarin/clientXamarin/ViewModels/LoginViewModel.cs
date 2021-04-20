using clientXamarin.Models;
using clientXamarin.Services;
using clientXamarin.View.MainContent;
using clientXamarin.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(INavigationService pageNavigation)
        {
            InitializeCommand();
            _pageNavigation = pageNavigation;
        }

        private const string Key = "accessToken";
        private string _username;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _loginResult;
        private readonly INavigationService _pageNavigation;

        public string  LoginResult
        {
            get => _loginResult;
            set => SetProperty(ref _loginResult, value);
        }

        private User _userToken;
        public User UserToken { get => _userToken; set => SetProperty(ref _userToken, value); }

        public ICommand LoginCommand { get; private set; }

        private void InitializeCommand()
        {
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            var response = await AccountService.LoginAsync(Username, Password);
            Preferences.Set("username", Username);
            Preferences.Set("password", Password);
            if (response)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                LoginResult = "Fail!";

            }
        }
    }
}
