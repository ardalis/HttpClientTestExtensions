using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions
{
  public static partial class HttpClientGetExtensionMethods
  {
    public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with GET {requestUri}");
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
      output?.WriteLine($"Requesting with GET {requestUri}");
      var response = await client.GetAsync(requestUri);
      response.EnsureNotFound();
      return response;
    }
  }
  public static partial class HttpClientPutExtensionMethods
  {
    public static async Task<HttpResponseMessage> PutAndEnsureNotFound(this HttpClient client, string requestUri, HttpContent content, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with PUT {requestUri}");
      var response = await client.PutAsync(requestUri, content);
      response.EnsureNotFound();
      return response;
    }
  }
  public static partial class HttpClientPostExtensionMethods
  {

    public static async Task<HttpResponseMessage> PostAndEnsureNotFound(this HttpClient client, string requestUri, HttpContent content, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with POST {requestUri}");
      var response = await client.PostAsync(requestUri, content);
      response.EnsureNotFound();
      return response;
    }
  }
  public static partial class HttpClientDeleteExtensionMethods
  {
    public static async Task<HttpResponseMessage> DeleteAndEnsureNotFound(this HttpClient client, string requestUri, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with DELETE {requestUri}");
      var response = await client.DeleteAsync(requestUri);
      response.EnsureNotFound();
      return response;
    }

    public static async Task<HttpResponseMessage> DeleteAndEnsureNoContent(this HttpClient client, string requestUri, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with DELETE {requestUri}");
      var response = await client.DeleteAsync(requestUri);
      response.EnsureNoContent();
      return response;
    }
  }
}
