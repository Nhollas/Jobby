using Ardalis.Specification;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Exceptions.Base;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Services;

public class ResourceProvider<TEntity> : 
    IResourceProvider<TEntity>, 
    IGetBySpec<TEntity>, 
    IGetById<TEntity>, 
    IApplySpecification<TEntity> 
    where TEntity : Entity
{
    private ISpecification<TEntity> _spec;
    private CancellationToken _cancellationToken = default;
    private Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> _getBySpec;
    private Func<Guid, CancellationToken, Task<TEntity>> _getById;

    public IGetBySpec<TEntity> GetBySpec(Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> getBySpec) 
    {
        _getBySpec = getBySpec;
        return this;
    } 

    public IGetById<TEntity> GetById(Func<Guid, CancellationToken, Task<TEntity>> getById) 
    {
        _getById = getById;
        return this;
    }

    public IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        _spec = spec;
        return this;
    }

    public async Task<TEntity> Check(string userId, Guid resourceId)
    {
        TEntity resource;

        if (_getById is null)
        {
            resource = await _getBySpec(_spec, _cancellationToken);
        }
        else
        {
            resource = await _getById(resourceId, _cancellationToken);
        }

        if (resource is null)
        {
            throw new NotFoundException($"The {nameof(TEntity)} Id {resourceId} could not be found.");
        }

        if (resource.OwnerId != userId)
        {
            throw new NotAuthorisedException(userId);
        }

        return resource;
    }
}
