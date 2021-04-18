using clientXamarin.Services;
using clientXamarin.Views.MainContentView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class ChatMainPageViewModel
    {
        private readonly INavigationService _navigationService;

        public ChatMainPageViewModel(INavigationService navigationService)
        {
            InitializeCommand();
            _navigationService = navigationService;
        }

        public ICommand SearchFriendCommand { get; private set; }

        private void InitializeCommand()
        {
            SearchFriendCommand = new Command(async () => await NavigateToSearchFriend());
        }

        private async Task NavigateToSearchFriend()
        {
            await _navigationService.PushAsync(new SearchFriendPage());
        }
    }
}
