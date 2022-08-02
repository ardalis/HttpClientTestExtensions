using System.Linq;
using Ardalis.Specification;
using Ardalis.HttpClientTestExtensions.Core.Entities;

namespace Ardalis.HttpClientTestExtensions.Core.Specifications;
public class CitiesOrderByNameSpec : Specification<City>
{
  public CitiesOrderByNameSpec()
  {
    Query
      .OrderBy(x => x.Name);
  }
}
