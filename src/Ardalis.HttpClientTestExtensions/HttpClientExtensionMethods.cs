using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions
{
    public static class HttpClientExtensionMethods
    {
        public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri, ITestOutputHelper output = null)
        {
            output?.WriteLine($"Requesting {requestUri}");
            var response = await client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            output?.WriteLine($"Response: {stringResponse}");
            var result = JsonSerializer.Deserialize<T>(stringResponse,
              Constants.DefaultJsonOptions);

            return result;
        }

        public static async Task<HttpResponseMessage> GetAndEnsureNotFound(this HttpClient client, string requestUri, ITestOutputHelper output = null)
        {
            output?.WriteLine($"Requesting {requestUri}");
            var response = await client.GetAsync(requestUri);
            response.EnsureNotFound();
            return response;
        }

        public static async Task<HttpResponseMessage> PutAndEnsureNotFound(this HttpClient client, string requestUri, HttpContent content, ITestOutputHelper output = null)
        {
            output?.WriteLine($"Requesting {requestUri}");
            var response = await client.PutAsync(requestUri, content);
            response.EnsureNotFound();
            return response;
        }

        public static async Task<HttpResponseMessage> PostAndEnsureNotFound(this HttpClient client, string requestUri, HttpContent content, ITestOutputHelper output = null)
        {
            output?.WriteLine($"Requesting {requestUri}");
            var response = await client.PostAsync(requestUri, content);
            response.EnsureNotFound();
            return response;
        }

        public static async Task<HttpResponseMessage> DeleteAndEnsureNotFound(this HttpClient client, string requestUri, ITestOutputHelper output = null)
        {
            output?.WriteLine($"Requesting {requestUri}");
            var response = await client.DeleteAsync(requestUri);
            response.EnsureNotFound();
            return response;
        }
    }
}
