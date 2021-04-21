using clientXamarin.Models;
using clientXamarin.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    class ChatRoomViewModel
    {
        private string _outgoingText = string.Empty;
        public string OutGoingText { get; set; }

        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ChatRoomViewModel()
        {
            InitializeCommand();
            Messages = new ObservableCollection<ChatMessage>();
        }

        public ICommand SendMessageCommand { get; private set; }

        private void InitializeCommand()
        {
            SendMessageCommand = new Command<string>(async (s) => await SendMessage(s));
        }

        private async Task SendMessage(string otherUsername)
        {
            await ChatService.Connect(otherUsername);
        }
    }
}
