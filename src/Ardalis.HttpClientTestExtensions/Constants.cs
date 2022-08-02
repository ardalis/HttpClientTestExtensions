using System.Text.Json;

namespace Ardalis.HttpClientTestExtensions;

public static class Constants
{
  public static JsonSerializerOptions DefaultJsonOptions = new JsonSerializerOptions
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
  };
}
