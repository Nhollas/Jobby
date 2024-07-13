using Ardalis.Specification;

namespace Jobby.Application.Interfaces.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
    Task<T?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default);
}
