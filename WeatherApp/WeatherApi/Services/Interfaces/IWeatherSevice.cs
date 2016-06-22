using System.Threading.Tasks;
using Services.Portable.DTO.Api;

namespace Services.Interfaces
{
    public interface IWeatherSevice
    {
        Task<CityWeatherResult> GetWeatherByCityNameAsync(string cityName);
        Task<CityWeatherResult> GetWeatherByCityIdAsync(int cityId);
        Task<CityWeatherForecastResult> GetWeatherForecastByCityNameAsync(string cityName);
        Task<CityWeatherForecastResult> GetWeatherForecastByCityIdAsync(int cityId);

    }
}