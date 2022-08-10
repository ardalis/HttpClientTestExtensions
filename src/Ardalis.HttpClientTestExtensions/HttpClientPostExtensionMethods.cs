using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPostExtensionMethods
{
  /// <summary>
  /// Ensures a POST to a requestUri returns a 404 Not Found response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
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

  /// <summary>
  /// Ensures a POST to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PostAndEnsureUnauthorizedAsync(this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with POST {requestUri}");
    var response = await client.PostAsync(requestUri, content);
    response.EnsureUnauthorized();
    return response;
  }

  /// <summary>
  /// Ensures a POST to a requestUri returns a 403 Forbidden response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PostAndEnsureForbiddenAsync(this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    output?.WriteLine($"Requesting with POST {requestUri}");
    var response = await client.PostAsync(requestUri, content);
    response.EnsureForbidden();
    return response;
  }
}
