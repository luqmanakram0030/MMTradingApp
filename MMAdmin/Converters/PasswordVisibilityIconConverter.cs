using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MMAdmin.Converters
{
    public class PasswordVisibilityIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible)
            {
                return isVisible ? "icon_eye_open.svg" : "icon_eye_closed.svg";
            }
            return "icon_eye_closed.svg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}