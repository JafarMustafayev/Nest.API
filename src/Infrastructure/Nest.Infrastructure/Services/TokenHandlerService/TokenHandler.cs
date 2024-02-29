using Microsoft.IdentityModel.Tokens;

namespace Nest.Infrastructure.Services.TokenHandlerService;

public class TokenHandler : ITokenHandler
{
    private readonly UserManager<AppUser> _userManager;

    public TokenHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public string CreateRefreshToken()
    {
        return $"{Guid.NewGuid().ToString()}--{Guid.NewGuid().ToString()}";
    }

    public async Task<Token> CreateTokenAsync(AppUser appUser)
    {
        var minutes = Int32.Parse(Configuration.JwtData["AccessTokenExpirationMinute"]);

        Token token = new()
        {
            Expiration = DateTime.UtcNow.AddMinutes(minutes),
        };

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Configuration.JwtData["Key"]));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Name, appUser.UserName),
        };

        var roles = await _userManager.GetRolesAsync(appUser);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityToken securityToken = new(
            audience: Configuration.JwtData["Audience"],
            issuer: Configuration.JwtData["Issuer"],
            expires: token.Expiration,
            signingCredentials: signingCredentials,
            claims: claims
            );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();

        return token;
    }
}