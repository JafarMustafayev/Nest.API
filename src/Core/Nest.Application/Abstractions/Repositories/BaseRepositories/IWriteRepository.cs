﻿namespace Nest.Application.Abstractions.Repositories.BaseRepositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    Task AddAsync(T entity);

    Task AddRangeAsync(ICollection<T> entities);

    void Remove(T entity);

    void RemoveRange(ICollection<T> entities);

    void Update(T entity);

    Task SaveChangesAsync();
}