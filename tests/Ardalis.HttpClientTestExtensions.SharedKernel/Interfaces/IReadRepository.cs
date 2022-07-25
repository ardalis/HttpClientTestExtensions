using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Ardalis.HttpClientTestExtensions.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
