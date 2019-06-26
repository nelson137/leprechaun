using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OktaTest
{
    public static class Extensions
    {
        public static async Task<T> PostJsonAsync<T>(this HttpClient _httpClient, string requestUri, object data)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize request data
            var jsonData = JsonConvert.SerializeObject(data);
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Make request
            var response = await _httpClient.PostAsync(requestUri, jsonContent);

            // Deserialize response data
            var str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
