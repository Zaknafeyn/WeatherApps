using Newtonsoft.Json;

namespace Services.Portable.DTO.Api.WeatherDetails
{
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}