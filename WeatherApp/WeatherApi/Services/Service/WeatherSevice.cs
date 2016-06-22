using System;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Portable;
using Services.Portable.API;
using Services.Portable.DTO;
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

        public async Task<CityWeatherResult> GetWeatherByCityNameAsync(string cityName)
        {
            var result = await _weatherApi.GetWeatherByCityNameAsync(cityName);
            return result;
        }

        public async Task<CityWeatherResult> GetWeatherByCityIdAsync(int cityId)
        {
            var result = await _weatherApi.GetWeatherByCityIdAsync(cityId);
            return result;
        }

        public async Task<CityWeatherResult> GetWeatherByCoordsAsync(Coordinates coords)
        {
            var results = await _weatherApi.GetWeatherByCoordAsync(coords);
            return results;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityNameAsync(string cityName)
        {
            var result = await _weatherApi.GetWeatherForecastByCityNameAsync(cityName);
            return result;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityIdAsync(int cityId)
        {
            var result = await _weatherApi.GetWeatherForecastByCityIdAsync(cityId);
            return result;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCoordsAsync(Coordinates coords)
        {
            var results = await _weatherApi.GetWeatherForecastByCoordsAsync(coords);
            return results;
        }
    }
}