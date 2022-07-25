using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class AddCityRequest
{
  public const string Route = "/cities";

  [Required]
  public string Name { get; set; } = string.Empty;
  public string? CountryId { get; set; }
}
