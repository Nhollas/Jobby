using Ardalis.Specification;

namespace Jobby.Application.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    Task<T?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default);
}
