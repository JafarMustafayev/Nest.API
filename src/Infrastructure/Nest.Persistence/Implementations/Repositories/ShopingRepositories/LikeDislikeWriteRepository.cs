namespace Nest.Persistence.Implementations.Repositories.ShopingRepositories;

public class LikeDislikeWriteRepository : WriteRepository<Likes>, ILikeDislikeWriteRepository
{
    public LikeDislikeWriteRepository(AppDbContext context) : base(context)
    {
    }
}