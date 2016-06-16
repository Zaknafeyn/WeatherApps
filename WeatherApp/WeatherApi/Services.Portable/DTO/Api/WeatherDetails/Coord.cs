using Newtonsoft.Json;

namespace Services.Portable.DTO.Api.WeatherDetails
{
    public class Coord
    {

        [JsonProperty("lon")]
        public decimal Lon { get; set; }

        [JsonProperty("lat")]
        public decimal Lat { get; set; }

    }
}