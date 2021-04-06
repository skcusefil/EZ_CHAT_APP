using clientXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.ViewModels
{
    class RegisterViewModel : BaseViewModel
    {
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

        private string _registerResult;

        private readonly INavigationService _pageNavigation;

        public RegisterViewModel(INavigationService pageNavigation)
        {
            _pageNavigation = pageNavigation;
            InitializeCommand();
        }

        public string RegisterResult
        {
            get => _registerResult;
            set => SetProperty(ref _registerResult, value);
        }


        public ICommand RegisterCommand { get; private set; }

        private void InitializeCommand()
        {
            RegisterCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            var response = await AccountService.RegisterAsync(Username, Password);
          
            if (response)
            {
                RegisterResult = "Sucessfully!";

                //await _pageNavigation.PushAsync(LoginPage());

            }
            else
            {
                RegisterResult = "Fail!";

            }
        }

        private Page LoginPage()
        {
            throw new NotImplementedException();
        }
    }
}
