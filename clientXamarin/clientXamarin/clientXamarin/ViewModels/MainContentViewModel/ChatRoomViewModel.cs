using clientXamarin.Models;
using clientXamarin.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class ChatRoomViewModel : BaseViewModel, IDisposable
    {
        private string _message = string.Empty;

        public string Message { get => _message; set => SetProperty(ref _message,value); }

        private ObservableCollection<ChatMessage> _messages;
        public ObservableCollection<ChatMessage> Messages { get=> _messages; set => SetProperty(ref _messages,value); }

        private IDisposable _messagesSubscription;
        private IDisposable _messageSubscription;


        private string _otherUsername;

        public string OtherUsername { get => _otherUsername; set => SetProperty(ref _otherUsername, value); }

        public string Title { get => "Chatting with " + OtherUsername; }

        public ChatRoomViewModel(string otherUsername)
        {
            _otherUsername = otherUsername;
            Connect();
            InitializeCommand();
        }

        public ICommand SendMessageCommand { get; private set; }

        private void InitializeCommand()
        {
            SendMessageCommand = new Command(() => SendMessage());
        }

        public void Init()
        {
            _messagesSubscription = ChatService.NewMeussagesReceived.Subscribe(GetMessages);
            _messageSubscription = ChatService.NewMeussageReceived.Subscribe(GetMessage);

    }

        private void GetMessage(ChatMessage message)
        {
            Messages.Add(message);
        }

        private void GetMessages(IEnumerable<ChatMessage> messages)
        {
            Messages = new ObservableCollection<ChatMessage>(messages);

        }

        private async void Connect()
        {
            await ChatService.Connect(_otherUsername);   
        }

        private async void SendMessage()
        {
            await ChatService.SendMessage(_otherUsername, _message);
            //set input empty after send
            Message = string.Empty;
        }

        public void Dispose()
        {
            _messagesSubscription.Dispose();
            _messageSubscription.Dispose();
        }
    }
}
