using Ardalis.Specification;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetBySpec<TEntity> where TEntity : Entity
{
    IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec);
}
