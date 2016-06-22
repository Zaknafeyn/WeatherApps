using System;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO.Api;
using Services.Portable.WeatherUriBuilder;

namespace Services.Service
{
    public class WeatherSevice : IWeatherSevice
    {

        private readonly WeatherApi _weatherApi;

        public WeatherSevice()
        {
            _weatherApi = new WeatherApi();
        }

        public async Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName)
        {
            var result = await _weatherApi.GetWeatherByCityNameAsync(cityName);
            return result;
        }

        public async Task<CityWeatherStatus> GetWeatherByCityIdAsync(int cityId)
        {
            var result = await _weatherApi.GetWeatherByCityIdAsync(cityId);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityNameAsync(string cityName)
        {
            var result = await _weatherApi.GetWeatherForecastByCityNameAsync(cityName);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityIdAsync(int cityId)
        {
            var result = await _weatherApi.GetWeatherForecastByCityIdAsync(cityId);
            return result;
        }
    }
}