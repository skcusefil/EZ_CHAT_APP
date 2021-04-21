using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace clientXamarin.Configurations
{
    public static class ServerConnectionString
    {
        public static readonly string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ?
                "https://10.0.2.2:5001/" : "https://localhost:5001/";

        public static string ConnectionString()
        {
            return RestUrl;
        }
    }
}
