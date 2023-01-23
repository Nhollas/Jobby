using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetBySpec<TEntity> where TEntity : Entity
{
    IGetBySpec<TEntity> GetBySpec(Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> getBySpec);
    IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec);
}
