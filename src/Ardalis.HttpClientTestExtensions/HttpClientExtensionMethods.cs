using System;
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
            var response = await client.GetAsync(requestUri);
            output?.WriteLine($"Requesting {requestUri}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            output?.WriteLine($"Response: {stringResponse}");
            var result = JsonSerializer.Deserialize<T>(stringResponse,
              Constants.DefaultJsonOptions);

            return result;
        }
    }
}
