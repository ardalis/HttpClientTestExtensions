using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPutExtensionMethods
{
  /// <summary>
  /// PUTs content to requestUri and asserts response is 404 Not Found.
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output"></param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndEnsureNotFoundAsync(this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with PUT {requestUri}");
    var response = await client.PutAsync(requestUri, content);
    response.EnsureNotFound();
    return response;
  }
}
