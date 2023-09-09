using Ardalis.Specification;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Services;

public class ResourceProvider<TEntity> : 
    IGetBySpec<TEntity>, 
    IGetById<TEntity>, 
    IApplySpecification<TEntity>
    where TEntity : Entity
{
    private ISpecification<TEntity> _spec;
    private Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> _getBySpec;
    private Func<Guid, CancellationToken, Task<TEntity>> _getById;
    
    public static IGetBySpec<TEntity> GetBySpec(Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> getBySpec) 
    {
        var provider = new ResourceProvider<TEntity>
        {
            _getBySpec = getBySpec
        };

        return provider;
    }

    public static IGetById<TEntity> GetById(Func<Guid, CancellationToken, Task<TEntity>> getById) 
    {
        var provider = new ResourceProvider<TEntity>
        {
            _getById = getById
        };

        return provider;
    }

    public IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        _spec = spec;
        return this;
    }

    public async Task<ResourceResult<TEntity>> Check(string userId, Guid resourceId, CancellationToken cancellationToken = default)
    {
        var resource = await _getById(resourceId, cancellationToken);

        if (resource is null)
        {
            return new ResourceResult<TEntity>(false, Outcome.NotFound, $"The {typeof(TEntity).Name} with Id {resourceId} could not be found.");
        }

        if (resource.OwnerId != userId)
        {
            return new ResourceResult<TEntity>(false, Outcome.Unauthorised, "You are not authorised to access this resource.");
        }
        
        return new ResourceResult<TEntity>(true, Outcome.Success, response: resource);
    }

    public async Task<ResourceResult<TEntity>> Check(string userId, CancellationToken cancellationToken = default)
    {
        var resource = await _getBySpec(_spec, cancellationToken);

        if (resource is null)
        {
            
            return new ResourceResult<TEntity>(false, Outcome.NotFound, $"The {typeof(TEntity).Name} with the provided spec could be found.");
        }

        if (resource.OwnerId != userId)
        {
            return new ResourceResult<TEntity>(false, Outcome.Unauthorised, "You are not authorised to access this resource.");
        }

        return new ResourceResult<TEntity>(true, Outcome.Success, response: resource);
    }
}
