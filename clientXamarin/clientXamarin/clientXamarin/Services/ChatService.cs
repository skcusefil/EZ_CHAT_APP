using clientXamarin.Configurations;
using clientXamarin.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.Services
{
    public class ChatService
    {
        private readonly static string localHost = "https://10.0.2.2:45455/hubs/";



        private  User _user;
        private string _otherUsername;
        private static HubConnection _connection;
        
        //public event EventHandler<ChatMessage> OnMessageReceived;

        public static ObservableCollection<ChatMessage> Messages { get; }

        public ChatService(User user, string otherUsername)
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
                                                // bypass SSL certificate
                                                clientHandler.ServerCertificateCustomValidationCallback +=
                                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                                            return message;
                                        };
                                    })
                                    .Build();


                await _connection.StartAsync();

                if(_connection.State.ToString() == "Connected")
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

        public static async Task Stop()
        {
             await _connection.StopAsync();
        }

        public static async Task SendMessage(string recipientUsername, string content)
        {
            await _connection.InvokeAsync("SendMessage", recipientUsername, content);
        }
    }
}
