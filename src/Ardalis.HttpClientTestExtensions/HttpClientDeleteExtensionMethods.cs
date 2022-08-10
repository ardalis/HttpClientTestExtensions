using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientDeleteExtensionMethods
{
  public static async Task<HttpResponseMessage> DeleteAndEnsureNotFoundAsync(this HttpClient client, string requestUri, ITestOutputHelper output = null)
  {
    var response = await client.DeleteAsync(requestUri, output);
    response.EnsureNotFound();
    return response;
  }

  public static async Task<HttpResponseMessage> DeleteAndEnsureNoContentAsync(this HttpClient client, string requestUri, ITestOutputHelper output = null)
  {
    var response = await client.DeleteAsync(requestUri, output);
    response.EnsureNoContent();
    return response;
  }

  public static async Task<string> DeleteAndEnsureSubstringAsync(this HttpClient client,
    string requestUri,
    string substring,
    ITestOutputHelper output = null)
  {
    var response = await client.DeleteAsync(requestUri, output);
    return await response.EnsureContainsAsync(substring);
  }

  public static async Task<HttpResponseMessage> DeleteAsync(
    this HttpClient client, 
    string requestUri, 
    ITestOutputHelper output)
  {
    output?.WriteLine($"Requesting with DELETE {requestUri}");
    return await client.DeleteAsync(requestUri);
  }
}
