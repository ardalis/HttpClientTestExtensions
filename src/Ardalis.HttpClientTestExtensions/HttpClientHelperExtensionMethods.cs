using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Testing.Handlers;
using Xunit.Abstractions;

namespace Ardalis.HttpClientTestExtensions;

public static partial class HttpClientHelperExtensionMethods
{
  /// <summary>
  /// Ensures that the HttpClient is not configured to automatically follow redirects.
  /// </summary>
  /// <param name="client"></param>
  /// <param name="output">Optional; used to provide details to standard output.</param>
  /// <example>
  /// <code>
  /// var client = _factory.CreateClient(new WebApplicationFactoryClientOptions() { AllowAutoRedirect = false });
  /// client.EnsureNoAutoRedirect();
  /// </code>
  /// </example>
  /// <returns></returns>
  public static void EnsureNoAutoRedirect(this HttpClient client, ITestOutputHelper output = null)
  {
    output?.WriteLine($"Ensuring HttpClient does not auto-redirect");
    var handler = client.GetType().BaseType.GetField("_handler", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(client);
    if (handler.GetType() == typeof(RedirectHandler)) 
    {
      throw new HttpRequestException("HttpClient is configured to follow redirects.");
    }
  }
}
