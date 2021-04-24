using clientXamarin.ViewModels.MainContentViewModel;
using System;
using System.Collections.Generic;
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

        public ChatRoomPage(string otherUsername)
        {
            InitializeComponent();
            _otherUsername = otherUsername;

            ViewModel = new ChatRoomViewModel(otherUsername);
            BindingContext = ViewModel;

        }

        public ChatRoomViewModel ViewModel { get; }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Init();
        }
    }
}