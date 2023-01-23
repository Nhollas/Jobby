using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IApplySpecification<TEntity> where TEntity : Entity
{
    IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec);
    Task<TEntity> Check(string userId, Guid resourceId);
}
