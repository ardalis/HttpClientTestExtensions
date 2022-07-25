using System.Collections.Generic;
using Ardalis.HttpClientTestExtensions.SharedKernel;
using Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

namespace Ardalis.HttpClientTestExtensions.Core.Entities;

public class Country : BaseEntity<string>, IAggregateRoot
{
  public string Name { get; set; } = string.Empty;
  public virtual List<City>? Cities { get; set; }

  public void AddCity(City city)
  {
    if (Cities == null)
    {
      Cities = new List<City>();
    }
    Cities.Add(city);
  }
}

