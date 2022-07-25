using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Dtos;

public class CityDto
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = string.Empty;
  public string? CountryId { get; set; }
}
