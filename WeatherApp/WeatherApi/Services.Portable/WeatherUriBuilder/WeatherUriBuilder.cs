using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Portable.WeatherUriBuilder
{
    public class WeatherUriBuilder
    {
        private const string ApiKey = "803dfac6bc748485d062419a84c770d4";
        private const string ApiBaseUri = "http://api.openweathermap.org/data/2.5";
        private readonly WeatherOptions _weatherOption;

        protected WeatherUriBuilder(WeatherOptions weatherOption)
        {
            _weatherOption = weatherOption;
        }

        public static WeatherUriBuilder GetForecastWeatherBuilder()
        {
            return new WeatherUriBuilder(WeatherOptions.Forecast);
        }

        public static WeatherUriBuilder GetCurrentWeatherBuilder()
        {
            return new WeatherUriBuilder(WeatherOptions.Current);
        }

        public string City { get; set; }

        public int? CityId { get; set; }

        private string GetWeatherApiPath()
        {
            switch (_weatherOption)
            {
                case WeatherOptions.Current:
                    return "weather";
                case WeatherOptions.Forecast:
                    return "forecast";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Uri Build()
        {
            if (!CityId.HasValue && string.IsNullOrEmpty(City))
                throw new InvalidOperationException("City and/or CityId is not specified. Cannot build query without these data.");

            var cityQueryParam = CityId?.ToString() ?? City;
            var paramDict = new Dictionary<string, string>
            {
                ["APPID"] = ApiKey
            };

            var key = CityId.HasValue ? "id" : "q";
            var value = CityId?.ToString() ?? cityQueryParam;
            paramDict.Add(key, value);

            var queryParams = paramDict.Select(x => $"{x.Key}={x.Value}");

            var uri = new Uri(ApiBaseUri).Append(GetWeatherApiPath()).AppendQuery(string.Join("&",queryParams.ToArray()));

            return uri;
        }
    }
}