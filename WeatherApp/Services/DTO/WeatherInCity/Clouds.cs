using Newtonsoft.Json;

namespace Services.DTO.WeatherInCity
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}