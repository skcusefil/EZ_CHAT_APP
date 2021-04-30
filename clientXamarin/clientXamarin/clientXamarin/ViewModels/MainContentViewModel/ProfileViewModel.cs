using clientXamarin.Interfaces;
using clientXamarin.Models;
using clientXamarin.Services;
using clientXamarin.Views.MainContentView;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.ViewModels.MainContentViewModel
{

    public class ProfileViewModel : BaseViewModel
    {

        private ImageSource _profileImage;

        public ImageSource ProfileImage { get => _profileImage; set => SetProperty(ref _profileImage, value); }

        private string _diplayName;
        private readonly INavigationService _pageNavigation;
        private readonly IMemberService _memberService = new MemberService();

        public string DisplayName { get=> _diplayName; set => SetProperty(ref _diplayName,value); }

        public ProfileViewModel(INavigationService pageNavigation)
        {
            InitializeCommand();
            GetCurrentUserInfo();
            _pageNavigation = pageNavigation;
        }

        public ICommand UploadCommand { get; private set; }
        public ICommand EditNameCommand { get; private set; }

        private void InitializeCommand()
        {
            UploadCommand = new Command(UploadImage);
            EditNameCommand = new Command<string>(async (s) => await NavigateToEditNamePage(s));
        }

        private async Task NavigateToEditNamePage(string displayName)
        {
            await _pageNavigation.PushAsync(new EditNamePage(displayName));
        }

        private async void GetCurrentUserInfo()
        {
            var userInfo = await _memberService.GetMember(Preferences.Get("username", ""));
            if (string.IsNullOrEmpty(userInfo.PhotoUrl))
            {
                ProfileImage = "user.png";
                DisplayName = "";
            }
            else
            {
                ProfileImage = userInfo.PhotoUrl;
                DisplayName = userInfo.DisplayName;
            }
        }

        private async void UploadImage()
        {
            var selectedImageFile = await MediaPicker.PickPhotoAsync();

            if (selectedImageFile == null)
            {
                return;
            }

            await PhotoService.AddPhoto(selectedImageFile);

            GetCurrentUserInfo();
        }

    }
}
