using Newtonsoft.Json;

namespace Services.DTO.WeatherInCity
{
    public class Wind
    {

        [JsonProperty("speed")]
        public double Speed { get; set; }

        [JsonProperty("deg")]
        public int Deg { get; set; }
    }
}