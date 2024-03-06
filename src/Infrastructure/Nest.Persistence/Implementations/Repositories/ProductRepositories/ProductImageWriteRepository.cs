namespace Nest.Persistence.Implementations.Repositories.ProductRepositories;

public class ProductImageWriteRepository : WriteRepository<ProductImage>, IProductImageWriteRepository
{
    public ProductImageWriteRepository(AppDbContext context) : base(context)
    {
    }
}