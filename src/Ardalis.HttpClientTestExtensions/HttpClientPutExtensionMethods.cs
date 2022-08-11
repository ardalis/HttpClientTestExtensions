using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPutExtensionMethods
{
  public static async Task<T> PutAndDeserializeAsync<T>(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureSuccessStatusCode();
    var stringResponse = await response.Content.ReadAsStringAsync();
    output?.WriteLine($"Response: {stringResponse}");
    var result = JsonSerializer.Deserialize<T>(stringResponse,
      Constants.DefaultJsonOptions);

    return result;
  }


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

  public static async Task<HttpResponseMessage> PutAsync(
    this HttpClient client, 
    string requestUri,
    HttpContent content, 
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with PUT {requestUri}");
    return await client.PutAsync(requestUri, content);
  }

}
