using Jobby.Application.Responses;
using Jobby.Application.Services;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IApplySpecification<TEntity> where TEntity : Entity
{
    Task<ResourceResult<TEntity>> Check(string userId, CancellationToken cancellationToken = default);
}
