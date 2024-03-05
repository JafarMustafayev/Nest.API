namespace Nest.Persistence.Implementations.Repositories.BaseRepositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(int page, int take, bool isTracking = true, Expression<Func<T, object>>? OrderBy = null, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null)
    {
        var query = Table.AsQueryable();

        query = isTracking ? Table : Table.AsNoTracking();

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

        if (OrderBy != null)
        {
            query = query.OrderBy(OrderBy);
        }

        query = query.Skip((page - 1) * take).Take(take);

        return query;
    }

    public IQueryable<T> GetAllByExpression(Expression<Func<T, bool>> expression, int page, int take, bool isTracking = true, Expression<Func<T, object>>? OrderBy = null, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null)
    {
        var query = Table.AsQueryable();

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

        if (OrderBy != null)
        {
            query = query.OrderBy(OrderBy);
        }

        query = query.Where(expression);

        query = query.Skip((page - 1) * take).Take(take);

        return query;
    }

    public async Task<T?> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null)
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

    public async Task<T?> GetByIdAsync(string? id = null, bool isTracking = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>>? include, Expression<Func<object, object>>? thenInclude)>? thenIncludes = null)
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