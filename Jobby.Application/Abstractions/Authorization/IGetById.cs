using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetById<TEntity> where TEntity : Entity
{
    IGetById<TEntity> GetById(Func<Guid, CancellationToken, Task<TEntity>> getById);
    Task<TEntity> Check(string userId, Guid resourceId);
}
