namespace Nest.Persistence.Implementations.Services;

internal class ShopingService : IShopingService
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILikeDislikeReadRepository _likeDislikeReadRepository;
    private readonly ILikeDislikeWriteRepository _likeDislikeWriteRepository;

    public ShopingService(IProductReadRepository productReadRepository,
                          UserManager<AppUser> userManager,
                          ILikeDislikeReadRepository likeDislikeReadRepository,
                          ILikeDislikeWriteRepository likeDislikeWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _userManager = userManager;
        _likeDislikeReadRepository = likeDislikeReadRepository;
        _likeDislikeWriteRepository = likeDislikeWriteRepository;
    }

    public async Task<ResponseDTO> LikeDislike(LikeAndUnLikeDTO likeDislIkeDTO)
    {
        var product = await _productReadRepository.GetByIdAsync(likeDislIkeDTO.ProductId);

        if (product == null)
        {
            throw new NotFoundCustomException("Product not found");
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == likeDislIkeDTO.UserRefreshToken);

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        var like = await _likeDislikeReadRepository.GetSingleByExpressionAsync(x => x.ProductId == likeDislIkeDTO.ProductId && x.UserId == user.Id);

        if (like == null)
        {
            await _likeDislikeWriteRepository.AddAsync(new Likes
            {
                IsLike = true,
                Product = product,
                User = user,
            });
        }
        else if (like != null && like.IsLike == true)
        {
            like.IsLike = !like.IsLike;
            like.IsDeleted = !like.IsDeleted;
            like.UpdatedAt = null;
            like.DeletedAt = DateTime.UtcNow;
            _likeDislikeWriteRepository.Update(like);
        }
        else
        {
            like.IsLike = !like.IsLike;
            like.IsDeleted = !like.IsDeleted;
            like.UpdatedAt = DateTime.UtcNow;
            like.DeletedAt = null;
            _likeDislikeWriteRepository.Update(like);
        }
        await _likeDislikeWriteRepository.SaveChangesAsync();

        return new()
        {
            Errors = null,
            Message = "the process is successful ",
            Payload = null,
            StatusCode = 200,
            Success = true
        };
    }

    public async Task<ResponseDTO> GetLikeDislikeByUser(string refreshToken, int page = 1)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        Expression<Func<Likes, bool>> expression = x => x.IsDeleted == false && x.UserId == user.Id && x.IsLike;
        //product id, product name, product price, product poster image

        var likes = _likeDislikeReadRepository.GetAllByExpression(expression, page, 20, false, x => x.UpdatedAt != null ? x.UpdatedAt : x.CreatedAt).Select(x => new GetLikedProductDTO
        {
            ProductId = x.Product.Id,
            ProductDiscount = x.Product.Discount,
            ProductName = x.Product.Name,
            ProductPrice = x.Product.Price,
            ProductImage = x.Product.ProductImages.Where(x => x.IsMain).FirstOrDefault().ImagePath
        });

        return new()
        {
            Errors = null,
            Message = "the process is successful",
            Payload = likes,
            StatusCode = 200,
            Success = true
        };
    }
}