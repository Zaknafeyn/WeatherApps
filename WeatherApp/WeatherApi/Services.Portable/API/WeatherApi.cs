using System.Threading.Tasks;
using Services.Portable.DTO.Api;

namespace Services.Portable.API
{
    public class WeatherApi
    {
        private readonly RestClient _restClient;

        public WeatherApi(RestClient restClient)
        {
            _restClient = restClient;
        }

        public WeatherApi()
        {
            _restClient = new RestClient();
        }

        public async Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherStatus>(uri);
            return result;
        }

        public async Task<CityWeatherStatus> GetWeatherByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherStatus>(uri);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecast>(uri);
            return result;
        }

        public async Task<CityWeatherForecast> GetWeatherForecastByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecast>(uri);
            return result;
        }
    }
}