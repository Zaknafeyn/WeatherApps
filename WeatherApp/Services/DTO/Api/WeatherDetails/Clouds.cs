using Newtonsoft.Json;

namespace Services.DTO.Api.WeatherDetails
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}