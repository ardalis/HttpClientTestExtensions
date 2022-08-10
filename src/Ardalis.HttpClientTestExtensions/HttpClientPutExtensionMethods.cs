using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPutExtensionMethods
{
  /// <summary>
  /// Ensures a PUT to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
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

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndEnsureUnauthorizedAsync(this HttpClient client,
  string requestUri,
  HttpContent content,
  ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with PUT {requestUri}");
    var response = await client.PutAsync(requestUri, content);
    response.EnsureUnauthorized();
    return response;
  }

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndEnsureForbiddenAsync(this HttpClient client,
  string requestUri,
  HttpContent content,
  ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with PUT {requestUri}");
    var response = await client.PutAsync(requestUri, content);
    response.EnsureForbidden();
    return response;
  }

}
