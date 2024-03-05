namespace Nest.Persistence.Implementations.Repositories.VendorRepositories;

public class VendorReadRepository : ReadRepository<Vendor>, IVendorReadRepository
{
    public VendorReadRepository(AppDbContext context) : base(context)
    {
    }
}