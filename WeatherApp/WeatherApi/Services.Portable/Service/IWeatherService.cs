using System.Threading.Tasks;
using Services.Portable.DTO;
using Services.Portable.DTO.Api;

namespace Services.Portable.Service
{
    public interface IWeatherService
    {
        Task<CityWeatherResult> GetWeatherByCityNameAsync(string cityName);
        Task<CityWeatherResult> GetWeatherByCityIdAsync(int cityId);
        Task<CityWeatherResult> GetWeatherByCoordsAsync(Coordinates coords);
        Task<CityWeatherForecastResult> GetWeatherForecastByCityNameAsync(string cityName);
        Task<CityWeatherForecastResult> GetWeatherForecastByCityIdAsync(int cityId);
        Task<CityWeatherForecastResult> GetWeatherForecastByCoordsAsync(Coordinates coords);
    }
}