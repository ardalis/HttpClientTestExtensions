namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class DeleteCityRequest
{
  public const string Route = "/cities/{id:int}";
  public static string BuildRoute(int cityId) => Route.Replace("{id:int}", cityId.ToString());
}
