using clientXamarin.Configurations;
using clientXamarin.Interfaces;
using clientXamarin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace clientXamarin.Services
{
    public class MemberService : IMemberService
    {

        public async Task<Member> GetMember(string username)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var response = await httpClient.GetStringAsync(ServerConnectionString.RestUrl + "api/user/"+ username);

            if (response == null)
            {
                await App.Current.MainPage.DisplayAlert("Notfound", "can not find user", "Cancel");
            }

            var result = JsonConvert.DeserializeObject<Member>(response);

            return result;
        }

        public async Task<bool> EditMember(string displayName)
        {
            var username = Preferences.Get("username", "");

            var updateMember = new MemberUpdate
            {
                DisplayName = displayName
            };

            var json = JsonConvert.SerializeObject(updateMember);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var response = await httpClient.PutAsync(ServerConnectionString.RestUrl + "api/user", content);

            if (!response.IsSuccessStatusCode) return false;

            return true;
        }
    }
}
