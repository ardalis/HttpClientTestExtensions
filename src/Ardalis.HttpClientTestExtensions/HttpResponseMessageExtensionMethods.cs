using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static class HttpResponseMessageExtensionMethods
{
  /// <summary>
  /// Ensures a response has a status code 404 Not Found
  /// </summary>
  /// <param name="response"></param>
  /// <return></return>
  public static void EnsureNotFound(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.NotFound);
  }

  /// <summary>
  /// Ensures a response has a status code 204 No Content
  /// </summary>
  /// <param name="response"></param>
  /// <return></return>
  public static void EnsureNoContent(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.NoContent);
  }

  /// <summary>
  /// Ensures a response has a status code 401 Unauthorized
  /// </summary>
  /// <param name="response"></param>
  /// <return></return>
  public static void EnsureUnauthorized(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.Unauthorized);
  }

  /// <summary>
  /// Ensures a response has a status code 403 Forbidden
  /// </summary>
  /// <param name="response"></param>
  /// <return></return>
  public static void EnsureForbidden(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.Forbidden);
  }

  /// <summary>
  /// Ensures a response has a status code 400 Bad Request
  /// </summary>
  /// <param name="response"></param>
  /// <return></return>
  public static void EnsureBadRequest(this HttpResponseMessage response)
  {
    response.Ensure(HttpStatusCode.BadRequest);
  }

  /// <summary>
  /// Ensures a response contains a substring
  /// </summary>
  /// <param name="response"></param>
  /// <param name="substring">The substring to look for</param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <return>The response string</return>
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
