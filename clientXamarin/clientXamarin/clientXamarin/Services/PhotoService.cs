using clientXamarin.Configurations;
using clientXamarin.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace clientXamarin.Services
{
    public class PhotoService
    {
        public static async Task<bool> AddPhoto(FileResult selectedImageFile)
        {
           // var selectedImageFile = await MediaPicker.PickPhotoAsync();

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(await selectedImageFile.OpenReadAsync()), "file", selectedImageFile.FileName);


            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));

            var response = await httpClient.PostAsync(ServerConnectionString.RestUrl + "api/user/add-photo",content);

            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
    }
}
