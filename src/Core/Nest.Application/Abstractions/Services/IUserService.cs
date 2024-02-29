namespace Nest.Application.Abstractions.Services;

public interface IUserService
{
    Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenExpiredDate, int addOnAccessTokenDate);

    Task<ResponseDTO> LogOut(string refreshToken);
}