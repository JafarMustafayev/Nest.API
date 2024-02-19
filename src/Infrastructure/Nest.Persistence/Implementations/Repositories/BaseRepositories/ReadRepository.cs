﻿
namespace Nest.Persistence.Implementations.Repositories.BaseRepositories;
public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool isTracking = true, bool forAdmin = true, List<Expression<Func<T, object>>> includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = null)
    {
        var query = isTracking ? Table : Table.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (thenIncludes != null)
        {
            foreach (var includePair in thenIncludes)
            {
                query = query.Include(includePair.include).ThenInclude(includePair.thenInclude);
            }
        }

        if (forAdmin)
        {
            return query;
        }
        else
        {
            return query.Where(x => x.IsDeleted == false);
        }
    }

    public IQueryable<T> GetAllByExpression(Expression<Func<T, bool>> expression, bool isTracking = true, bool forAdmin = true, List<Expression<Func<T, object>>> includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = null)
    {
        var query = Table.Where(expression).AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (thenIncludes != null)
        {
            foreach (var includePair in thenIncludes)
            {
                query = query.Include(includePair.include).ThenInclude(includePair.thenInclude);
            }
        }

        query = isTracking ? query : query.AsNoTracking();

        if (forAdmin)
        {
            return query;
        }
        else
        {
            return query.Where(x => x.IsDeleted == false);
        }
    }

    public async Task<T?> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, List<Expression<Func<T, object>>> includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = null)
    {
        var query = isTracking ? Table : Table.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (thenIncludes != null)
        {
            foreach (var includePair in thenIncludes)
            {
                query = query.Include(includePair.include).ThenInclude(includePair.thenInclude);
            }
        }
        var res = await query.FirstOrDefaultAsync(expression);

        return res;
    }

    public async Task<T?> GetByIdAsync(string id, bool isTracking = true, List<Expression<Func<T, object>>> includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)> thenIncludes = null)
    {
        var query = isTracking ? Table : Table.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (thenIncludes != null)
        {
            foreach (var includePair in thenIncludes)
            {
                query = query.Include(includePair.include).ThenInclude(includePair.thenInclude);
            }
        }

        var res = await query.FirstOrDefaultAsync(x => x.Id == id);
        return res;
    }
}