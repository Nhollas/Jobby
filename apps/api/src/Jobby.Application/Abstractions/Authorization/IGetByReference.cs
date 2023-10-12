using Jobby.Application.Responses;
using Jobby.Application.Services;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetByReference<TEntity> where TEntity : Entity
{
    Task<ResourceResult<TEntity>> Check(string userId, string resourceReference, CancellationToken cancellationToken = default);
}
