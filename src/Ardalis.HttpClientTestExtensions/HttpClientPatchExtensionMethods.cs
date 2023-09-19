using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientPatchExtensionMethods
{
  /// <summary>
  /// Makes a PATCH request to a requestUri and deserializes the response to a T object
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The deserialized response object</returns>
  public static async Task<T> PatchAndDeserializeAsync<T>(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    response.EnsureSuccessStatusCode();
    var stringResponse = await response.Content.ReadAsStringAsync();
    output?.WriteLine($"Response: {stringResponse}");
    var result = JsonSerializer.Deserialize<T>(stringResponse,
      Constants.DefaultJsonOptions);

    return result;
  }

  /// <summary>
  /// Ensures a PATCH to a requestUri returns a 404 Not Found response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PatchAndEnsureNotFoundAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    response.EnsureNotFound();
    return response;
  }

  /// <summary>
  /// Ensures a PATCH to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PatchAndEnsureUnauthorizedAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    response.EnsureUnauthorized();
    return response;
  }

  /// <summary>
  /// Ensures a PATCH to a requestUri returns a 403 Forbidden response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PatchAndEnsureForbiddenAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    response.EnsureForbidden();
    return response;
  }

  /// <summary>
  /// Makes a PATCH request to a requestUri and ensures the response contains a substring
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="substring"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The response string</returns>
  public static async Task<string> PatchAndEnsureSubstringAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    string substring,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    return await response.EnsureContainsAsync(substring);
  }

  /// <summary>
  /// Ensures a PATCH to a requestUri returns a 400 Bad Request response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PatchAndEnsureBadRequestAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    response.EnsureBadRequest();
    return response;
  }

  /// <summary>
  /// Ensures a PATCH to a requestUri returns a 302 Redirect response status code
  /// and redirects to the expected redirectUri
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="content"></param>
  /// <param name="redirectUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> PatchAndRedirectAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    string redirectUri,
    ITestOutputHelper output = null)
  {
    var response = await client.PatchAsync(requestUri, content, output);
    client.EnsureNoAutoRedirect(output);
    response.EnsureRedirect(redirectUri);
    return response;
  }

  private static async Task<HttpResponseMessage> PatchAsync(
    this HttpClient client,
    string requestUri,
    HttpContent content,
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with PATCH {requestUri}");
    return await client.PatchAsync(requestUri, content);
  }
}
