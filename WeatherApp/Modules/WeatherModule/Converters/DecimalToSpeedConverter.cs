using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherModule.Converters
{
    public class DecimalToSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var speed = (decimal) value;
            return $"{speed} mph";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}