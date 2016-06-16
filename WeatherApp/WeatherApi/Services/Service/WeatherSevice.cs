using System;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Portable;
using Services.Portable.DTO.Api;
using Services.Portable.WeatherUriBuilder;

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
            var uriBuilder = WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherStatus>(uri);
            return result;
        }

        public async Task<CityWeatherStatus> GetWeatherByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherStatus>(uri);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecast>(uri);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecast>(uri);
            return result;
        }
    }
}