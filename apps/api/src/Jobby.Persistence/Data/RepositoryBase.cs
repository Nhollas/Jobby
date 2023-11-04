using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Jobby.Application.Abstractions.Specification;
using Jobby.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace Jobby.Persistence.Data;

public abstract class RepositoryBase<T> : IRepository<T>, IReadRepository<T> where T : Entity
{
    private readonly DbContext _dbContext;
    private readonly Tracer _tracer;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    protected RepositoryBase(DbContext dbContext, Tracer tracer)
        : this(dbContext, SpecificationEvaluator.Default, tracer)
    {
    }
    
    private RepositoryBase(DbContext dbContext, ISpecificationEvaluator specificationEvaluator, Tracer tracer)
    {
        _dbContext = dbContext;
        _specificationEvaluator = specificationEvaluator;
        _tracer = tracer;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var span = _tracer.StartActiveSpan("AddAsync");
        span?.SetAttribute("EntityId", entity.Id.ToString());
        span?.SetAttribute("Entity-Reference", entity.Reference);
        span?.SetAttribute("Entity-Type", typeof(T).Name);
        
        _dbContext.Set<T>().Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var addRangeAsync = entities as T[] ?? entities.ToArray();
        _dbContext.Set<T>().AddRange(addRangeAsync);

        await SaveChangesAsync(cancellationToken);

        return addRangeAsync;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var span = _tracer.StartActiveSpan("UpdateAsync");
        span?.SetAttribute("EntityId", entity.Id.ToString());
        span?.SetAttribute("Entity-Reference", entity.Reference);
        span?.SetAttribute("Entity-Type", typeof(T).Name);
        
        _dbContext.Set<T>().Update(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        using var span = _tracer.StartActiveSpan("DeleteAsync");
        span?.SetAttribute("EntityId", entity.Id.ToString());
        span?.SetAttribute("Entity-Reference", entity.Reference);
        span?.SetAttribute("Entity-Type", typeof(T).Name);
        
        _dbContext.Set<T>().Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(specification);
        _dbContext.Set<T>().RemoveRange(query);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    public async Task<T> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    public async Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification, true).CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().CountAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AnyAsync(cancellationToken);
    }

    public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
    {
        return ApplySpecification(specification).AsAsyncEnumerable();
    }

    /// <summary>
    /// Filters the entities  of <typeparamref name="T"/>, to those that match the encapsulated query logic of the
    /// <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="evaluateCriteriaOnly"></param>
    /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);
    }

    /// <summary>
    /// Filters all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// <para>
    /// Projects each entity into a new form, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
    }

    public async Task<T> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        using var span = _tracer.StartActiveSpan("GetByReferenceAsync");
        span?.SetAttribute("Entity-Reference", reference);
        span?.SetAttribute("Entity-Type", typeof(T).Name);
        
        return await _dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Reference == reference, cancellationToken);
    }
}
