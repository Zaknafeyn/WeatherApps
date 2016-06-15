using System;
using System.Linq;
using Services.DTO.Api;
using Services.DTO.Api.WeatherForecast;
using WeatherModule.Models;

namespace WeatherModule.DataConverters
{
    internal static class WeatherConverters
    {
        internal static WeatherForecastModel Convert(this WeatherForecastItem source)
        {
            var weather = source.Weather.First();
            var dayOrNight = weather.Icon.EndsWith("d") ? "d" : "n";

            return new WeatherForecastModel
            {
                TempAvg = source.Main.Temp.NormalizeTemperature(),
                TempMax = source.Main.TempMax.NormalizeTemperature(),
                TempMin = source.Main.TempMin.NormalizeTemperature(),
                Day = source.Dt.DayOfWeek.ToString(),
                WeatherDescr = weather.Description,
                Wind = source.Wind.Speed,
                IconUri = new Uri(
                    $"pack://application:,,,/WeatherModule;component/Resources/Icons/WeatherIcons/{weather.Id}{dayOrNight}.png")
            };
        }
    }
}