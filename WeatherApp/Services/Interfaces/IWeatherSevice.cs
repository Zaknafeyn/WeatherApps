using System.Threading.Tasks;
using Services.DTO;
using Services.DTO.Api;

namespace Services.Interfaces
{
    public interface IWeatherSevice
    {
        Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName);
        Task<CityWeatherForecast> GetWeatherForecast(string cityName);
    }
}