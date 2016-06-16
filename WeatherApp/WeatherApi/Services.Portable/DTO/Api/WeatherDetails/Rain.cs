using Newtonsoft.Json;

namespace Services.Portable.DTO.Api.WeatherDetails
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double LastThreeHoursLevel { get; set; }
    }
}