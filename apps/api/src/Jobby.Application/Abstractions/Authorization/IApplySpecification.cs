using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IApplySpecification<TEntity> where TEntity : Entity
{
    Task<TEntity> Check(string userId, CancellationToken cancellationToken = default);
}
