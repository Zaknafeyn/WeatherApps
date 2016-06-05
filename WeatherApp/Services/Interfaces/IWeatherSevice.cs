using System.Threading.Tasks;
using Services.DTO.WeatherInCity;

namespace Services.Interfaces
{
    public interface IWeatherSevice
    {
        Task<CityWeatherStatus> GetWeatherByCityNameAsync(string cityName);
    }
}