using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;

public interface IResource<TEntity>
    where TEntity : Entity
{
    void WithUser(string userId);
    void TargetResourceId(Guid resourceId);
    void FindWith(Delegate repositoryMethod);
    void ApplySpecification(ISpecification<TEntity> spec);
    Task<TEntity> Check();
}
