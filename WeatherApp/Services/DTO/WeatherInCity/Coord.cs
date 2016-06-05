using Newtonsoft.Json;

namespace Services.DTO.WeatherInCity
{
    public class Coord
    {

        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

    }
}