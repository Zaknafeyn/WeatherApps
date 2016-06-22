using Newtonsoft.Json;

namespace Services.Portable.DTO.Api.WeatherDetails
{
    public class Coord
    {
        [JsonProperty("lon")]
        public decimal Longtitude { get; set; }

        [JsonProperty("lat")]
        public decimal Latitude { get; set; }

    }
}