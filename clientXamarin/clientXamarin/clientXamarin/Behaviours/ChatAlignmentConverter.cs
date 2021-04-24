using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.Behaviours
{
    public class ChatAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                string textOwner = value.ToString();
                var user = Preferences.Get("username", "");
                var otherUsername = Preferences.Get("otherUsername", "");

                if(textOwner == user)
                {
                    return LayoutOptions.End;
                }
                else 
                {
                    return LayoutOptions.Start;
                }
                
            }
            else
            {
                return LayoutOptions.Start;
            }
         
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
