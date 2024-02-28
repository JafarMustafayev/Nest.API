namespace Nest.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    public Task<ResponseDTO> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Token> LoginAsync(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Token> RefreshTokenLoginAsync(string token)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO)
    {
        throw new NotImplementedException();
    }
}