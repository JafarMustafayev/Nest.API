

namespace Nest.Persistence.Implementations.Repositories.BaseRepositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task AddRangeAsync(ICollection<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public void Remove(T entity)
    {
        Table.Remove(entity);
    }

    public void RemoveRange(ICollection<T> entities)
    {
        Table.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new Exception("Error while saving changes");
        }
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }
}
