using Ardalis.Specification;

namespace Jobby.Core.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
