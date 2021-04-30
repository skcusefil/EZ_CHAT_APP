using clientXamarin.Services;
using clientXamarin.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace clientXamarin.ViewModels
{
    public class MainViewModel
    {
        private readonly INavigationService _pageNavigation;

        public MainViewModel(INavigationService pageNavigation)
        {
            _pageNavigation = pageNavigation;

            InitializeCommand();
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }

        private void InitializeCommand()
        {
            LoginCommand = new Command(async () => await NavigateToLoginPage());
            RegisterCommand = new Command(async () => await NavigateToRegisterPage());
        }

        private async Task NavigateToRegisterPage()
        {
            await _pageNavigation.PushAsync(new RegisterPage());
        }

        private async Task NavigateToLoginPage()
        {
            await _pageNavigation.PushAsync(new LoginPage());
        }
    }
}
