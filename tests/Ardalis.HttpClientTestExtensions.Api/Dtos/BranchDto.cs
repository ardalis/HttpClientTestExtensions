using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Dtos;
public class BranchDto
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
  public string? Phone { get; set; }
  public string? Email { get; set; }
  public string? CityName { get; set; }
  public int CityId { get; set; }
}
