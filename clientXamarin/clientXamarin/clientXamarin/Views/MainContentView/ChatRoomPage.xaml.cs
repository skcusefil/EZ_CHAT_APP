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
        public ChatRoomPage()
        {
            InitializeComponent();
            BindingContext = new ChatRoomViewModel();
        }
    }
}