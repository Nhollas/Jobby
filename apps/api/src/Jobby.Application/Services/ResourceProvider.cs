using Ardalis.Specification;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Responses;
using Jobby.Domain.Primitives;

namespace Jobby.Application.Services;

public class ResourceProvider<TEntity> : 
    IGetBySpec<TEntity>, 
    IGetByReference<TEntity>, 
    IApplySpecification<TEntity>
    where TEntity : Entity
{
    private ISpecification<TEntity> _spec;
    private Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> _getBySpec;
    private Func<string, CancellationToken, Task<TEntity>> _getByReference;
    
    public static IGetBySpec<TEntity> GetBySpec(Func<ISpecification<TEntity>, CancellationToken, Task<TEntity>> getBySpec) 
    {
        var provider = new ResourceProvider<TEntity>
        {
            _getBySpec = getBySpec
        };

        return provider;
    }

    public static IGetByReference<TEntity> GetByReference(Func<string, CancellationToken, Task<TEntity>> getByReference) 
    {
        var provider = new ResourceProvider<TEntity>
        {
            _getByReference = getByReference
        };

        return provider;
    }

    public IApplySpecification<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        _spec = spec;
        return this;
    }

    public async Task<ResourceResult<TEntity>> Check(string userId, string resourceReference, CancellationToken cancellationToken = default)
    {
        var resource = await _getByReference(resourceReference, cancellationToken);

        if (resource is null)
        {
            return new ResourceResult<TEntity>(false, Outcome.NotFound, $"The {typeof(TEntity).Name} with Reference {resourceReference} could not be found.");
        }

        return resource.OwnerId != userId ? new ResourceResult<TEntity>(false, Outcome.Unauthorised, "You are not authorised to access this resource.") : new ResourceResult<TEntity>(true, Outcome.Success, response: resource);
    }

    public async Task<ResourceResult<TEntity>> Check(string userId, CancellationToken cancellationToken = default)
    {
        var resource = await _getBySpec(_spec, cancellationToken);

        if (resource is null)
        {
            
            return new ResourceResult<TEntity>(false, Outcome.NotFound, $"The {typeof(TEntity).Name} with the provided spec could be found.");
        }

        return resource.OwnerId != userId ? new ResourceResult<TEntity>(false, Outcome.Unauthorised, "You are not authorised to access this resource.") : new ResourceResult<TEntity>(true, Outcome.Success, response: resource);
    }
}
