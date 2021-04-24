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
        public ICommand NavigateToChatCommand { get; private set; }

        private void InitializeCommand()
        {
            SearchFriendCommand = new Command<string>(async (s) => await SearchFriend(s));
            NavigateToChatCommand = new Command<string>(async (u) => await NavigateToChatRoom(u));
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
                var member = await MemberService.GetMember(s);

                var otherUsername = member.Username;
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }

        }
    }
}
