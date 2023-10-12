using Ardalis.Specification.EntityFrameworkCore;
using Jobby.Application.Abstractions.Specification;
using Jobby.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Data;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : Entity
{
    private readonly JobbyDbContext _dbContext;
    public Repository(JobbyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Reference == reference, cancellationToken);
    }
}
