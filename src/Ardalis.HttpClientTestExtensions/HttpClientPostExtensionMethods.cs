using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPostExtensionMethods
{
  /// <summary>
  /// POSTs content to requestUri and asserts response is 404 Not Found.
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output"></param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PostAndEnsureNotFoundAsync(this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with POST {requestUri}");
    var response = await client.PostAsync(requestUri, content);
    response.EnsureNotFound();
    return response;
  }
}
