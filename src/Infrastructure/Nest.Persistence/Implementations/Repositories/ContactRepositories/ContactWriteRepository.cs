namespace Nest.Persistence.Implementations.Repositories.ContactRepositories;

public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteReposiyory
{
    public ContactWriteRepository(AppDbContext context) : base(context)
    {
    }
}