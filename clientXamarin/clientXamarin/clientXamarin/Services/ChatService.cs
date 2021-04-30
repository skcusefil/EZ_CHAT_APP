using clientXamarin.Configurations;
using clientXamarin.Interfaces;
using clientXamarin.Models;
using clientXamarin.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.Services
{
    public class ChatService : IChatServices
    {
        private string _otherUsername;
        private static HubConnection _connection;

        public static ObservableCollection<ChatMessage> _chats { get; set; } = new ObservableCollection<ChatMessage>();

        private static Subject<IEnumerable<ChatMessage>> _newMessages = new Subject<IEnumerable<ChatMessage>>();

        public static IObservable<IEnumerable<ChatMessage>> NewMeussagesReceived => _newMessages;

        private static Subject<ChatMessage> _newMessage = new Subject<ChatMessage>();
        public static IObservable<ChatMessage> NewMeussageReceived => _newMessage;

        public ChatService(string otherUsername)
        {
            _otherUsername = otherUsername;
        }


        public async Task<bool> Connect(string otherUsername)
        {
            try
            {
                var accessToken = Preferences.Get("accessToken", "");
                _connection = new HubConnectionBuilder()
                                    .WithUrl(ServerConnectionString.RestUrl + "hubs/chat?user=" + _otherUsername, options =>
                                    {
                                        options.AccessTokenProvider = () => Task.FromResult(accessToken);
                                        options.HttpMessageHandlerFactory = (message) =>
                                        {
                                            if (message is HttpClientHandler clientHandler)
                                                // bypass SSL certificate for running in localhost, without this will be SSL error
                                                clientHandler.ServerCertificateCustomValidationCallback +=
                                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                                            return message;
                                        };
                                    })
                                    .Build();


                await _connection.StartAsync();

                _connection.On<IEnumerable<ChatMessage>>("ReceiveMessageThread", data =>

                {
                    foreach (var item in data)
                    {
                        if (item.SenderPhotoUrl == null)
                        {
                            item.SenderPhotoUrl = "user.png";
                        }
                    }
                    _newMessages.OnNext(data);

                });

                _connection.On<ChatMessage>("NewMessage", data =>
                {
                    _newMessage.OnNext(data);
                });

                _connection.On<Group>("UpdatedGroup", group =>
                {
                    if (group.Connections.Any(x => x.Username == otherUsername))
                    {
                        foreach (var chat in _chats)
                        {
                            chat.DateRead = DateTime.UtcNow;
                        }
                    }
                });

                if (_connection.State == HubConnectionState.Connected)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Cancel");
            }

            return false;
        }

        public async Task Stop()
        {
            await _connection.StopAsync();
        }

        public async Task SendMessage(string otherUsername, string message)
        {
            await _connection.InvokeAsync<CreateMessage>("SendMessage", new CreateMessage { RecipientUsername = otherUsername, Content = message });
        }
    }
}
