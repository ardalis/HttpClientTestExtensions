using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static class HttpResponseMessageExtensionMethods
{
  public static void EnsureNotFound(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.NotFound);
  }

  public static void EnsureNoContent(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.NoContent);
  }

  public static void EnsureUnauthorized(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.Unauthorized);
  }

  public static void EnsureForbidden(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.Forbidden);
  }

  public static void EnsureBadRequest(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.BadRequest);
  }

  public static async Task<string> EnsureContainsAsync(
    this HttpResponseMessage response,
    string substring,
    ITestOutputHelper output = null)
  {
    var responseString = await response.Content.ReadAsStringAsync();
    output?.WriteLine($"Ensuring substring \"{substring}\" in response \"{responseString}\"");
    if (!responseString.Contains(substring))
    {
      throw new HttpRequestException($"Expected substring \"{substring}\" not found in response \"{responseString}\"");
    };

    return responseString;
  }

  private static void Ensure(this HttpResponseMessage response, HttpStatusCode expected)
  {
    if (response.StatusCode != expected)
    {
      ThrowHelper(expected, response.StatusCode);
    }
  }

  private static HttpRequestException ThrowHelper(HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode)
  {
    throw new HttpRequestException($"Expected {expectedStatusCode.ToString("D")} {expectedStatusCode.ToString("G")} but was {actualStatusCode.ToString("D")} {actualStatusCode.ToString("G")}");
  }
}
