using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;

public interface IWithResource<TEntity> where TEntity : Entity
{
    IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec);
}