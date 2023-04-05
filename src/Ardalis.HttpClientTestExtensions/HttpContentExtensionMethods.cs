using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Ardalis.HttpClientTestExtensions;

public static class HttpContentExtensionMethods
{
  public static StringContent FromModelAsJson(this HttpContent content, object model)
  {
    return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"));
  }
}
