using System.Collections.Generic;

namespace Ardalis.HttpClientTestExtensions.SharedKernel;

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity<T>
{
  public T? Id { get; set; }
}
