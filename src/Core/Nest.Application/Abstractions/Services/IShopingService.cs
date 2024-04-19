namespace Nest.Application.Abstractions.Services;

public interface IShopingService
{
    Task<ResponseDTO> LikeDislike(LikeAndUnLikeDTO likeDislIkeDTO);

    Task<ResponseDTO> GetLikeDislikeByUser(string refreshToken, int page);
}