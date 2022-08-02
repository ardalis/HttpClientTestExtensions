using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CityEndpoints;

public class EditCityRequest
{
  public const string Route = "/cities";

  [Required]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = string.Empty;
  public string? CountryId { get; set; }
}
