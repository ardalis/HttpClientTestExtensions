namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class DeleteCountryRequest
{
  public const string Route = "/countries/{id}";
  public static string BuildRoute(string id) => Route.Replace("{id}", id);
}
