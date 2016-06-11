using Newtonsoft.Json;

namespace Services.DTO.Api.WeatherDetails
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double LastThreeHoursLevel { get; set; }
    }
}