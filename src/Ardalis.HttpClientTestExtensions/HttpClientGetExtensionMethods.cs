using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientGetExtensionMethods
{
  /// <summary>
  /// Makes a GET request to a requestUri and deserializes the response to a T object
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The deserialized response object</returns>
  public static async Task<T> GetAndDeserializeAsync<T>(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureSuccessStatusCode();
    var stringResponse = await response.Content.ReadAsStringAsync();
    output?.WriteLine($"Response: {stringResponse}");
    var result = JsonSerializer.Deserialize<T>(stringResponse,
      Constants.DefaultJsonOptions);

    return result;
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 404 Not Found response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndEnsureNotFoundAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureNotFound();
    return response;
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 405 Method Not Allowed response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndEnsureMethodNotAllowedAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureMethodNotAllowed();
    return response;
  }

  /// <summary>
  /// Makes a GET request to a requestUri and returns the response string
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The response string</returns>
  public static async Task<string> GetAndReturnStringAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    return await response.Content.ReadAsStringAsync();
  }

  /// <summary>
  /// Makes a GET request to a requestUri and ensures the response contains a substring
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="substring">The substring to look for in the response string</param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns>The response string</returns>
  public static async Task<string> GetAndEnsureSubstringAsync(
    this HttpClient client,
    string requestUri,
    string substring,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    return await response.EnsureContainsAsync(substring);
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 401 Unauthorized response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndEnsureUnauthorizedAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureUnauthorized();
    return response;
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 403 Forbidden response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndEnsureForbiddenAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureForbidden();
    return response;
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 400 Bad Request response status code
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndEnsureBadRequestAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureBadRequest();
    return response;
  }

  /// <summary>
  /// Ensures a GET to a requestUri returns a 302 Redirect response status code
  /// and redirects to the expected redirectUri
  /// </summary>
  /// <param name="client"></param>
  /// <param name="requestUri"></param>
  /// <param name="redirectUri"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <returns></returns>
  public static async Task<HttpResponseMessage> GetAndRedirectAsync(
    this HttpClient client,
    string requestUri,
    string redirectUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    client.EnsureNoAutoRedirect(output);
    response.EnsureRedirect(redirectUri);
    return response;
  }

  private static async Task<HttpResponseMessage> GetAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with GET {requestUri}");
    return await client.GetAsync(requestUri);
  }
}
