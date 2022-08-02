using System.Linq;
using Ardalis.Specification;
using Ardalis.HttpClientTestExtensions.Core.Entities;

namespace Ardalis.HttpClientTestExtensions.Core.Specifications;
public class CountriesOrderByNameSpec : Specification<Country>
{
  public CountriesOrderByNameSpec()
  {
    Query
      .OrderBy(x => x.Name);
  }
}
