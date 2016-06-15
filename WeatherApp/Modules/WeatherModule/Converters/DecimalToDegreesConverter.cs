using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherModule.Converters
{
    public class DecimalToDegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var celsiumDegrees = (decimal) value;

            return $"{celsiumDegrees}º";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}