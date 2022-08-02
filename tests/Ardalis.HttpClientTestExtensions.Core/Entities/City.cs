using System.Collections.Generic;
using Ardalis.HttpClientTestExtensions.SharedKernel;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Core.Entities;

public class City : BaseEntity<int>, IAggregateRoot
{
  public string Name { get; set; } = string.Empty;
  public string? CountryId { get; set; }
  public virtual Country? Country { get; set; }
}
