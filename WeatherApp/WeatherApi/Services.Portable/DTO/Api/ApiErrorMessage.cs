using Newtonsoft.Json;

namespace Services.Portable.DTO.Api
{
    public class ApiErrorMessage
    {
        [JsonProperty("cod")]
        public string Cod { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}