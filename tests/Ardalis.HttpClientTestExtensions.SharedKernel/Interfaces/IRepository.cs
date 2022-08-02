using Ardalis.Specification;

namespace Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
