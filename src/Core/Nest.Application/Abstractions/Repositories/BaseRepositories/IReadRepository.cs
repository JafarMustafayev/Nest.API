

namespace Nest.Application.Abstractions.Repositories.BaseRepositories;

public interface IReadRepository<T>: IRepository<T> where T :  BaseEntity, new()
{
    public IQueryable<T?> GetAll(bool isTracking = true, bool forAdmin = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>>thenInclude)>? thenIncludes = null);

    public IQueryable<T?> GetAllByExpression(Expression<Func<T, bool>> exception, bool isTracking = true, bool forAdmin = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null);

    Task<T?> GetByIdAsync(string id, bool isTracking = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null);

    public Task<T?> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression, bool isTracking = true, List<Expression<Func<T, object>>>? includes = null, List<(Expression<Func<T, object>> include, Expression<Func<object, object>> thenInclude)>? thenIncludes = null);
}