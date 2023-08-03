using Ardalis.Specification;

namespace Jobby.Application.Abstractions.Specification;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
