using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPutExtensionMethods
{
  /// <summary>
  /// Makes a PUT request to a requestUri and deserializes the response to a T object
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The deserialized response object</returns>
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
  /// Ensures a PUT to a requestUri returns a 404 Not Found response status code
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
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureNotFound();
    return response;
  }

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 405 Method Not Allowed response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndEnsureMethodNotAllowedAsync(this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureMethodNotAllowed();
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
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureUnauthorized();
    return response;
  }

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 403 Forbidden response status code
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
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureForbidden();
    return response;
  }

  /// <summary>
  /// Makes a PUT request to a requestUri and ensures the response contains a substring
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="substring"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The response string</returns>
  public static async Task<string> PutAndEnsureSubstringAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    string substring,
    ITestOutputHelper output = null)
  {
    var response = await client.PutAsync(requestUri, content, output);
    return await response.EnsureContainsAsync(substring);
  }

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 400 Bad Request response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndEnsureBadRequestAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PutAsync(requestUri, content, output);
    response.EnsureBadRequest();
    return response;
  }

  /// <summary>
  /// Ensures a PUT to a requestUri returns a 302 Redirect response status code
  /// and redirects to the expected redirectUri
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="redirectUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PutAndRedirectAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    string redirectUri,
    ITestOutputHelper output = null)
  {
    var response = await client.PutAsync(requestUri, content, output);
    client.EnsureNoAutoRedirect(output);
    response.EnsureRedirect(redirectUri);
    return response;
  }

  private static async Task<HttpResponseMessage> PutAsync(
    this HttpClient client, 
    string requestUri,
    HttpContent content, 
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with PUT {requestUri}");
    return await client.PutAsync(requestUri, content);
  }

}
