namespace Nest.Persistence.Implementations.Repositories.VendorRepositories;

public class VendorWriteRepository : WriteRepository<Vendor>, IVendorWriteRepository
{
    public VendorWriteRepository(AppDbContext context) : base(context)
    {
    }
}