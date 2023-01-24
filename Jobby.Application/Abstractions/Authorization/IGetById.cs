using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetById<TEntity> where TEntity : Entity
{
    Task<TEntity> Check(string userId, Guid resourceId);
}
