using Jobby.Domain.Primitives;

namespace Jobby.Application.Abstractions.Authorization;
public interface IGetBySpec<TEntity> where TEntity : Entity
{
    IWithResource<TEntity> WithResource(string resourceReference);
}
