using System.Threading.Tasks;
using Services.Portable.DTO.Api;

namespace Services.Interfaces
{
    public interface IWeatherSevice
    {
        Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName);
        Task<CityWeatherStatus> GetWeatherByCityIdAsync(int cityId);
        Task<CityWeatherForecast> GetWeatherForecastByCityNameAsync(string cityName);
        Task<CityWeatherForecast> GetWeatherForecastByCityIdAsync(int cityId);

    }
}