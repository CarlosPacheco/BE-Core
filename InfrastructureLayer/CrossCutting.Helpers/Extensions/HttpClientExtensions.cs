using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrossCutting.Helpers.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient httpClient, string requestUri, T data)
        {
            return httpClient.PutAsync(requestUri, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, MediaTypeNames.Application.Json));
        }
    }
}
