using System.Collections.Generic;
using Newtonsoft.Json;
using Services.DTO.Api.WeatherDetails;
using Services.DTO.Api.WeatherForecast;

namespace Services.DTO.Api
{
    public class CityWeatherForecast
    {
        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("cod")]
        public string Cod { get; set; }

        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("cnt")]
        public int Cnt { get; set; }

        [JsonProperty("list")]
        public IList<WeatherForecastItem> HourlyForecast { get; set; }
    }
}