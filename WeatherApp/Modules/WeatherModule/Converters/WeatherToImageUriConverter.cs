﻿using System;
using System.Globalization;
using System.Windows.Data;
using Services.DTO.WeatherInCity;
using WeatherModule.Resources;

namespace WeatherModule.Converters
{
    public class WeatherToImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var weatherObj = value as CityWeatherStatus;

            if (weatherObj == null)
                return null;

                return GetImageUri(weatherObj);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private Uri GetImageUri(CityWeatherStatus weatherStatus)
        {
            if (weatherStatus.Clouds.All < 20)
            {
                // no clouds 
                return ImageSources.SunImage;
            }
            else if (weatherStatus.Clouds.All < 60)
            {
                // partially cloudy
                return ImageSources.PartlyCloudyDayImage;
            }
            else
            {
                return ImageSources.CloudsImage;
                // cloudy
            }
        }
    }
}