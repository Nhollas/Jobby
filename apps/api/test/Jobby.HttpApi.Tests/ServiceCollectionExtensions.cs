using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jobby.HttpApi.Tests;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        ServiceDescriptor? dbContextOptionsService = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<T>));
        
        if (dbContextOptionsService != null)
        {
            services.Remove(dbContextOptionsService);
        }

        
        ServiceDescriptor? dbContextService = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (dbContextService != null)
        {
            services.Remove(dbContextService);
        }
    }
}