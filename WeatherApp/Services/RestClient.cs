using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services
{
    public class RestClient
    {
        HttpClient restClient = new HttpClient();

        public async Task<TMessage> GetAsync<TMessage>(Uri uri)
        {
            var response = await restClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var result = JsonConvert.DeserializeObject<TMessage>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}