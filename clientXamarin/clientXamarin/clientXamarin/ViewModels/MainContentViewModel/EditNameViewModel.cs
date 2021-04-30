using clientXamarin.Interfaces;
using clientXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class EditNameViewModel : BaseViewModel
    {
        private string _displayName;
        private readonly INavigationService _navigationService;
        private readonly IMemberService _memberService = new MemberService();

        public string DisplayName { get=> _displayName; set => SetProperty(ref _displayName, value); }

        public EditNameViewModel(string displayName, INavigationService navigationService)
        {
            DisplayName = displayName;
            _navigationService = navigationService;
            InitializeCommand();
        }

        public ICommand SaveCommand { get; private set; }

        private void InitializeCommand()
        {
            SaveCommand = new Command(async() => await Save());
        }

        private async Task Save()
        {
            await _memberService.EditMember(_displayName);
            await _navigationService.PopAsync();
        }
    }
}
