using clientXamarin.Services;
using clientXamarin.Views.MainContentView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class ChatMainPageViewModel : BaseViewModel
    {

        private string _user;
        public string User { get => _user; set => SetProperty(ref _user, value); }

        private readonly INavigationService _navigationService;
        private readonly string _displayName;

        public string DisplayName { get => _displayName; }


        public ChatMainPageViewModel(INavigationService navigationService)
        {
            InitializeCommand();
            _navigationService = navigationService;
            _displayName = Preferences.Get("displayName", "");
        }

        public ICommand SearchFriendCommand { get; private set; }

        private void InitializeCommand()
        {
            SearchFriendCommand = new Command<string>(async (s) => await SearchFriend(s));
        }

        private async Task SearchFriend(string s)
        {
            var member = await MemberService.GetMembers(s);

            var text = "DisplayName: " + member.DisplayName + ", username: " + member.Username;
            await App.Current.MainPage.DisplayAlert("Member", "Found" + text, "Cancel");
        }
    }
}
