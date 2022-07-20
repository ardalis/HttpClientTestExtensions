using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions
{
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
}
