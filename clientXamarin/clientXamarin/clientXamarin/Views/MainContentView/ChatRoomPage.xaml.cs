using clientXamarin.Controls;
using clientXamarin.Interfaces;
using clientXamarin.Services;
using clientXamarin.ViewModels.MainContentViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace clientXamarin.Views.MainContentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatRoomPage : ContentPage
    {
        private readonly string _otherUsername;
        IChatServices _chatServices;

        public ChatRoomPage(string otherUsername, IChatServices chatServices)
        {
            InitializeComponent();
            _otherUsername = otherUsername;
            _chatServices = new ChatService(_otherUsername);

            ViewModel = new ChatRoomViewModel(_otherUsername, _chatServices);
            chatListView.ScrollToLast();

        }

        public ChatRoomViewModel ViewModel
        {
            get => BindingContext as ChatRoomViewModel;
            set
            {
                BindingContext = value;
            }
        }

        protected override void OnAppearing()
        {
            chatListView.ScrollToLast();

            base.OnAppearing();
            ViewModel.Init();

        }
    }
}