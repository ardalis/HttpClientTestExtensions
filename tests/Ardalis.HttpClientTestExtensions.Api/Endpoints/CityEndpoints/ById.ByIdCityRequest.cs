namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class ByIdCityRequest
{
  public const string Route = "/cities/{id:int}";
  public static string BuildRoute(int id) => Route.Replace("{id:int}", id.ToString());
}
