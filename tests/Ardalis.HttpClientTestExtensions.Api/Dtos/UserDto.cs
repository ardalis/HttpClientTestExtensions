using System;
using System.ComponentModel.DataAnnotations;

namespace Ardalis.HttpClientTestExtensions.Api.Dtos;
public class UserDto
{
  public Guid Id { get; set; }
  [Required]
  public string Username { get; set; } = string.Empty;
  public string? Password { get; set; } = string.Empty;
  public bool IsActive { get; set; }
  public int UserInfoId { get; set; }
  public UserInfoDto? UserInfo { get; set; }
}
