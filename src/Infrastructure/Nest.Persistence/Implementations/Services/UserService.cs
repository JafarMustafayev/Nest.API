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
}