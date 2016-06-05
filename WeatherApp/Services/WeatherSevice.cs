using System;
using System.Threading.Tasks;
using Services.DTO.WeatherInCity;
using Services.Interfaces;

namespace Services
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
            var apiKey = "803dfac6bc748485d062419a84c770d4";
            var uriStr = $"http://api.openweathermap.org/data/2.5/weather?q={cityName},uk&APPID={apiKey}";
            var result = await _restClient.GetAsync<CityWeatherStatus>(new Uri(uriStr));
            return result;
        }
    }
}