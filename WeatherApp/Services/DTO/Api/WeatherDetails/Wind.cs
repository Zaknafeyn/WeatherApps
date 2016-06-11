using Newtonsoft.Json;

namespace Services.DTO.Api.WeatherDetails
{
    public class Wind
    {

        [JsonProperty("speed")]
        public decimal Speed { get; set; }

        [JsonProperty("deg")]
        public decimal Deg { get; set; }
    }
}