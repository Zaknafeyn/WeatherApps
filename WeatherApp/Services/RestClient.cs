using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.DTO;
using Services.DTO.Api;
using Services.Exceptions;

namespace Services
{
    public class RestClient
    {
        HttpClient restClient = new HttpClient();

        public async Task<TMessage> GetAsync<TMessage>(Uri uri)
        {
            var response = await restClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            try
            {
                if (content.Contains("Error"))
                    throw new JsonSerializationException();

                var result = JsonConvert.DeserializeObject<TMessage>(content);
                return result;
            }
            catch (JsonSerializationException serializationEx)
            {
                var errorMessage = JsonConvert.DeserializeObject<ApiErrorMessage>(content);
                throw new ApiErrorException(errorMessage.Message, errorMessage.Cod, serializationEx);
            }
        }
    }
}