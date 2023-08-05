using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jobby.HttpApi.Tests;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var dbContextOptionsService = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<T>));
        
        if (dbContextOptionsService != null)
        {
            services.Remove(dbContextOptionsService);
        }

        
        var dbContextService = services.SingleOrDefault(d => d.ServiceType == typeof(T));
        if (dbContextService != null)
        {
            services.Remove(dbContextService);
        }
    }
    
    public static void RemoveJwtAuthentication(this IServiceCollection services)
    {
        services.RemoveAll<JwtBearerOptions>();
    }

    
    public static async Task EnsureDbCreatedAsync<T>(this IServiceCollection services) where T : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<T>();
        await context.Database.EnsureCreatedAsync();
    }
}