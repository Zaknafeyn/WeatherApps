using System.Threading.Tasks;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;

namespace Services.Portable.API
{
    internal class WeatherApi
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

        public async Task<CityWeatherResult> GetWeatherByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherResult>(uri);
            return result;
        }

        public async Task<CityWeatherResult> GetWeatherByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherResult>(uri);
            return result;
        }

        public async Task<CityWeatherResult> GetWeatherByCoordAsync(Coordinates coords)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetCurrentWeatherBuilder();
            uriBuilder.Coordinates = coords;

            var uri = uriBuilder.Build();

            var result = await _restClient.GetAsync<CityWeatherResult>(uri);
            return result;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityNameAsync(string cityName)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.City = cityName;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecastResult>(uri);
            return result;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCityIdAsync(int cityId)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.CityId = cityId;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecastResult>(uri);
            return result;
        }

        public async Task<CityWeatherForecastResult> GetWeatherForecastByCoordsAsync(Coordinates coords)
        {
            var uriBuilder = WeatherUriBuilder.WeatherUriBuilder.GetForecastWeatherBuilder();
            uriBuilder.Coordinates = coords;

            var uri = uriBuilder.Build();
            var result = await _restClient.GetAsync<CityWeatherForecastResult>(uri);
            return result;
        }
    }
}