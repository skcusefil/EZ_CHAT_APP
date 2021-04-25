using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace clientXamarin.Behaviours
{
    class BooleanConvertertextOtherUser : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string textOwner = value.ToString();
                var user = Preferences.Get("username", "");
                if (textOwner != user)
                {
                    return true;
                }

                return false;

            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}