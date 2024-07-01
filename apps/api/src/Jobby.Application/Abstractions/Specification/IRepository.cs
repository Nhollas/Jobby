using Ardalis.Specification;

namespace Jobby.Application.Abstractions.Specification;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    Task<T?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default);
}
