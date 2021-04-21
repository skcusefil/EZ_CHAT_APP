using clientXamarin.Configurations;
using clientXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnixTimeStamp;
using Xamarin.Essentials;

namespace clientXamarin.Services
{
    public class AccountService
    {
        public static async Task<bool> LoginAsync(string username, string password)
        {
            var loginModel = new LoginModel()
            {
                Username = username,
                Password = password
            };



            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            var httpClient = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ServerConnectionString.RestUrl + "api/account/login", content);

            if (!response.IsSuccessStatusCode) return false;

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<User>(jsonResult);
            Preferences.Set("accessToken", result.Token);
            Preferences.Set("user", result.Username);
            Preferences.Set("displayName", result.DisplayName);
            Preferences.Set("photoUrl", result.Url);

            return true;
        }

        public static async Task<bool> RegisterAsync(string username, string password)
        {
            var registerModel = new RegisterModel()
            {
                Username = username,
                Password = password
            };

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var httpClient = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ServerConnectionString.RestUrl + "api/account/register", content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }

    }
}
