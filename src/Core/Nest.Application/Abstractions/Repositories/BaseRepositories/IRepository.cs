namespace Nest.Application.Abstractions.Repositories.BaseRepositories;

public interface IRepository<T> where T: BaseEntity
{
    public DbSet<T> Table { get; }
}
