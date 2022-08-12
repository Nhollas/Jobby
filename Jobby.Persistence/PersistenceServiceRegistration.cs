using Jobby.Core.Interfaces;
using Jobby.Persistence.Data;
using Jobby.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<JobbyContext>(c =>
            c.UseSqlServer(configuration.GetConnectionString("JobbyConnection")));

        services.AddDbContext<JobbyIdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("JobbyIdentityConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));

        return services;
    }
}
