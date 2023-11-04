using Jobby.Domain.Primitives;
using OpenTelemetry.Trace;

namespace Jobby.Persistence.Data;

public class Repository<T> : RepositoryBase<T> where T : Entity
{
    public Repository(JobbyDbContext dbContext, Tracer tracer) : base(dbContext, tracer)
    {

    }
}