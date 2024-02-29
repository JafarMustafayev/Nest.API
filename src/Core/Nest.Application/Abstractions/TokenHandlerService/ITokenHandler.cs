namespace Nest.Application.Abstractions.TokenHandlerService;

public interface ITokenHandler
{
    Task<Token> CreateTokenAsync(AppUser appUser);

    string CreateRefreshToken();
}