namespace Nest.Persistence.Implementations.Repositories.ContactRepositories;

public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
{
    public ContactReadRepository(AppDbContext context) : base(context)
    {
    }
}