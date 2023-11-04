using Jobby.Persistence.Data;

namespace Jobby.HttpApi.Tests.Helpers;

public static class SeedDataHelper<T> where T : class
{
    public static async Task<T> AddAsync(T entity, JobbyDbContext context)
    {
        await context.Set<T>().AddAsync(entity);
        
        await context.SaveChangesAsync();
        
        return entity;
    }
    
    public static async Task<List<T>> AddRangeAsync(List<T> entities, JobbyDbContext context)
    {
        await context.Set<T>().AddRangeAsync(entities);
        
        await context.SaveChangesAsync();

        return entities;
    }

    public static async Task RemoveAsync(T entity, JobbyDbContext disposeContext)
    {
        disposeContext.Set<T>().Remove(entity);
        
        await disposeContext.SaveChangesAsync();
    }
}