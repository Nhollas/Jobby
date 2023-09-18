using Jobby.Application.Responses;
using Jobby.Application.Services;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetById<TEntity> where TEntity : Entity
{
    Task<ResourceResult<TEntity>> Check(string userId, Guid resourceId, CancellationToken cancellationToken = default);
}
