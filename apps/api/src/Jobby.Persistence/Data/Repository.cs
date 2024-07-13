using Ardalis.Specification.EntityFrameworkCore;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Jobby.Persistence.Data;

public class Repository<T>(JobbyDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T>, IReadRepository<T>
    where T : Entity
{
    public async Task<T?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Reference == reference, cancellationToken);
    }
}