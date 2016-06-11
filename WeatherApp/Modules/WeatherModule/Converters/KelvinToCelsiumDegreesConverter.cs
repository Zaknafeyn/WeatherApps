﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherModule.Converters
{
    public class KelvinToCelsiumDegreesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var kelvinValue = (decimal) value;
            var celsiumDegrees = kelvinValue.NormalizeTemperature();

            return celsiumDegrees;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}