using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Endpoints.CountryEndpoints;

public class EditCountryRequest
{
  public const string Route = "/countries";

  [Required]
  public string Id { get; set; } = string.Empty;
  [Required]
  public string Name { get; set; } = string.Empty;
}
