using Ardalis.Specification;
using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Exceptions.Base;
using Jobby.Domain.Primitives;

namespace Jobby.Persistence.Data;

public class ResourceProvider<TEntity> : Repository<TEntity>, IResource<TEntity>
    where TEntity : Entity
{
    private string _userId;
    private Guid _resourceId;
    private Delegate _repositoryMethod;
    private ISpecification<TEntity> _spec;
    private CancellationToken _cancellationToken = default;

    public ResourceProvider(JobbyContext dbContext) : base(dbContext)
    {
    }

    public void ApplySpecification(ISpecification<TEntity> spec)
    {
        _spec = spec;
    }

    public async Task<TEntity> Check()
    {
        TEntity resource = null;

        if (_spec is null)
        {

        }
        else
        {

        }

        if (resource is null)
        {
            throw new NotFoundException($"A {nameof(TEntity)} with Id {_resourceId} could not be found.");
        }

        if (resource.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        return resource;
    }

    public void FindWith(Delegate repositoryMethod)
    {
        _repositoryMethod = repositoryMethod;
    }

    public void TargetResourceId(Guid resourceId)
    {
        _resourceId = resourceId;
    }

    public void WithUser(string userId)
    {
        _userId = userId;
    }
}
