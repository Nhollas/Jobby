using Ardalis.Specification.EntityFrameworkCore;
using Jobby.Core.Interfaces;

namespace Jobby.Persistence.Data;

public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public Repository(JobbyContext dbContext) : base(dbContext)
    {
    }
}
