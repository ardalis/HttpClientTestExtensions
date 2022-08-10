using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientGetExtensionMethods
{
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

  public static async Task<HttpResponseMessage> GetAndEnsureNotFoundAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureNotFound();
    return response;
  }

  public static async Task<string> GetAndReturnStringAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    return await response.Content.ReadAsStringAsync();
  }

  public static async Task<string> GetAndEnsureSubstringAsync(
    this HttpClient client,
    string requestUri,
    string substring,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    return await response.EnsureContainsAsync(substring);
  }

  public static async Task<HttpResponseMessage> GetAndEnsureUnauthorizedAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureUnauthorized();
    return response;
  }

  public static async Task<HttpResponseMessage> GetAndEnsureForbiddenAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureForbidden();
    return response;
  }

  public static async Task<HttpResponseMessage> GetAndEnsureBadRequestAsync(
    this HttpClient client,
    string requestUri,
    ITestOutputHelper output = null)
  {
    var response = await client.GetAsync(requestUri, output);
    response.EnsureBadRequest();
    return response;
  }

  public static async Task<HttpResponseMessage> GetAsync(
    this HttpClient client, 
    string requestUri, 
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with GET {requestUri}");
    return await client.GetAsync(requestUri);
  }
}
