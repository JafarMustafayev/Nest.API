using Nest.Application.Abstractions.Services;

namespace Nest.Persistence.Implementations.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;

    public UserService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenExpiredDate, int addOnAccessTokenDate)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiredDate = accessTokenExpiredDate.AddMinutes(addOnAccessTokenDate);

        await _userManager.UpdateAsync(user);
    }

    public async Task<ResponseDTO> LogOut(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        if (user == null)
        {
            throw new InvalidOperationCustomException("Invalid refresh token");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiredDate = null;

        await _userManager.UpdateAsync(user);

        return new ResponseDTO
        {
            Message = "Log out successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}