using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IResourceProvider<TEntity> where TEntity : Entity
{
    IGetBySpec<TEntity> GetBySpec(Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> getBySpec);
    IGetById<TEntity> GetById(Func<Guid, CancellationToken, Task<TEntity>> getById);
}
