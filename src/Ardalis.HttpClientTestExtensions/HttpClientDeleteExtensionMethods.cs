using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions
{
	public static partial class HttpClientDeleteExtensionMethods
  {
    public static async Task<HttpResponseMessage> DeleteAndEnsureNotFound(this HttpClient client, string requestUri, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with DELETE {requestUri}");
      var response = await client.DeleteAsync(requestUri);
      response.EnsureNotFound();
      return response;
    }

    public static async Task<HttpResponseMessage> DeleteAndEnsureNoContentAsync(this HttpClient client, string requestUri, ITestOutputHelper output = null)
    {
      output?.WriteLine($"Requesting with DELETE {requestUri}");
      var response = await client.DeleteAsync(requestUri);
      response.EnsureNoContent();
      return response;
    }
  }
}
