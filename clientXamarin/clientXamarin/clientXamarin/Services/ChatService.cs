using clientXamarin.Configurations;
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
    public class ChatService
    {
        private  string _user;
        private string _otherUsername;
        private static HubConnection _connection;
 
        public static ObservableCollection<ChatMessage> _chats { get; set; } = new ObservableCollection<ChatMessage>();

        private static Subject<IEnumerable<ChatMessage>> _newMessage = new Subject<IEnumerable<ChatMessage>>();

        public static IObservable<IEnumerable<ChatMessage>> NewMeussageReceived => _newMessage;


        public ChatService(string user, string otherUsername)
        {
            _user = user;
            _otherUsername = otherUsername;
        }


        public static async Task<bool> Connect(string otherUsername)
        {
            try
            {
                var accessToken = Preferences.Get("accessToken", "");
                _connection = new HubConnectionBuilder()
                                    .WithUrl(ServerConnectionString.RestUrl + "hubs/chat?user=" + otherUsername, options =>
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
                    _newMessage.OnNext(data);

                    
                   
                    //chats = data;
                });

                _connection.On<ChatMessage>("NewMessage", data =>
                {
                    _chats.Add(data);
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

            foreach (var item in _chats)
            {
                Debug.WriteLine(item.Content);
            }

            return false;
        }

        public static async Task Stop()
        {
             await _connection.StopAsync();
        }

        public static async Task SendMessage(string otherUsername, string message)
        {
            var createMessage = new CreateMessage
            {
                RecipientUsername = otherUsername,
                Content = message
            };


            await _connection.InvokeAsync<CreateMessage>("SendMessage", new CreateMessage { RecipientUsername=otherUsername, Content = message});
        }


    }
}
