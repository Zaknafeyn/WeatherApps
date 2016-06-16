using Newtonsoft.Json;

namespace Services.Portable.DTO.Api.WeatherDetails
{
    public class Main
    {

        [JsonProperty("temp")]
        public decimal Temp { get; set; }

        [JsonProperty("pressure")]
        public decimal Pressure { get; set; }

        [JsonProperty("humidity")]
        public decimal Humidity { get; set; }

        [JsonProperty("temp_min")]
        public decimal TempMin { get; set; }

        [JsonProperty("temp_max")]
        public decimal TempMax { get; set; }
    }
}