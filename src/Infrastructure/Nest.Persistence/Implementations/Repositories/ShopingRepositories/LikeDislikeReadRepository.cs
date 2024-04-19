namespace Nest.Persistence.Implementations.Repositories.ShopingRepositories;

public class LikeDislikeReadRepository : ReadRepository<Likes>, ILikeDislikeReadRepository
{
    public LikeDislikeReadRepository(AppDbContext context) : base(context)
    {
    }
}