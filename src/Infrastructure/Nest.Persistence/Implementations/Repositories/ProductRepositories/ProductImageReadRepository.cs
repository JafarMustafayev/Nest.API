namespace Nest.Persistence.Implementations.Repositories.ProductRepositories;

public class ProductImageReadRepository : ReadRepository<ProductImage>, IProductImageReadRepository
{
    public ProductImageReadRepository(AppDbContext context) : base(context)
    {
    }
}