using Ardalis.Specification.EntityFrameworkCore;
using Jobby.Application.Abstractions.Specification;

namespace Jobby.Persistence.Data;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public Repository(JobbyDbContext dbContext) : base(dbContext)
    {
    }
}
