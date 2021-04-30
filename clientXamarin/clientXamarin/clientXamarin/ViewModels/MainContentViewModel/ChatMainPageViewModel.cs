using clientXamarin.Interfaces;
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

        private readonly INavigationService _navigationService;
        private readonly IMemberService _memberService = new MemberService();

        private string _displayName;
        public string DisplayName { get => _displayName; set => SetProperty(ref _displayName, value); }

        private string _username;
        public string Username { get => _username; }


        public ChatMainPageViewModel(INavigationService navigationService)
        {
            InitializeCommand();
            _navigationService = navigationService;
            _username = Preferences.Get("username", "");
            GetUserInfo();
        }

        public ICommand SearchFriendCommand { get; private set; }
        public ICommand NavigateToChatCommand { get; private set; }

        private void InitializeCommand()
        {
            SearchFriendCommand = new Command<string>(async (s) => await SearchFriend(s));
            NavigateToChatCommand = new Command<string>(async (u) => await NavigateToChatRoom(u));
        }

        private async void GetUserInfo()
        {
            var user = await _memberService.GetMember(_username);
            DisplayName = user.DisplayName;
        }

        private async Task NavigateToChatRoom(string otherUsername)
        {
            await _navigationService.PushAsync(new ChatRoomPage(otherUsername));
            Preferences.Set("ohterUsername", otherUsername);
        }

        private async Task SearchFriend(string s)
        {
            try
            {
                var member = await _memberService.GetMember(s);

                var otherUsername = member.Username;
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }

        }
    }
}
