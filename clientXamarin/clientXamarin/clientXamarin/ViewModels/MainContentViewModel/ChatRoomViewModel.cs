using clientXamarin.Controls;
using clientXamarin.Interfaces;
using clientXamarin.Models;
using clientXamarin.Services;
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
using Xamarin.Forms;

namespace clientXamarin.ViewModels.MainContentViewModel
{
    public class ChatRoomViewModel : BaseViewModel, IDisposable
    {
        public bool ShowScrollTap { get; set; } = false;
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        private string _message = string.Empty;


        public string Message { get => _message; set => SetProperty(ref _message,value); }

        private ObservableCollection<ChatMessage> _messages;
        public ObservableCollection<ChatMessage> Messages { get=> _messages; set => SetProperty(ref _messages,value); }

        private IDisposable _messagesSubscription;
        private IDisposable _messageSubscription;

        private ImageSource _imageOtherUser;
        public ImageSource ImagerOtherUser { get => _imageOtherUser; set => SetProperty(ref _imageOtherUser,value); }

        private string _otherUsername;
        private readonly IChatServices _chatServices;

        public string OtherUsername { get => _otherUsername; set => SetProperty(ref _otherUsername, value); }

        public string Title { get => "Chatting with " + OtherUsername; }

        public ChatRoomViewModel(string otherUsername, IChatServices chatServices)
        {
            _otherUsername = otherUsername;
            this._chatServices = chatServices;
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
            await _chatServices.Connect(_otherUsername);
            //Messages = new ObservableCollection<ChatMessage>();
            Messages = ChatService._chats;
        }

        private async void SendMessage()
        {
            if (Message == "")
            {
                return;
            }
            await _chatServices.SendMessage(_otherUsername, Message);
            Message = string.Empty;
        }

        public void Dispose()
        {
            _messagesSubscription.Dispose();
            _messageSubscription.Dispose();
        }
    }
}
