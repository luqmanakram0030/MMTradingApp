using System;
using System.Globalization;

namespace MMAdmin.Converters
{
    public class MonthNameFromNumber : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is int monthNo)
                {
                    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNo);
                }
                else
                {
                    return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
                }
            }
            catch (Exception ex)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Only one way bindings are supported with this converter");
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

