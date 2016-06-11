using System;
using System.Threading.Tasks;
using Services.DTO;
using Services.DTO.Api;
using Services.Interfaces;

namespace Services.Service
{
    public class WeatherSevice : IWeatherSevice
    {
        private readonly RestClient _restClient;

        public WeatherSevice(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherStatus>(uri);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecast(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecast>(uri);
            return result;
        }
    }
}