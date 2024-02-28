namespace Nest.Application.Abstractions.Services;

public interface IAuthService
{
    Task<Token> LoginAsync(LoginDTO loginDTO);

    Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO);

    Task<ResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);

    Task<ResponseDTO> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO);

    Task<Token> RefreshTokenLoginAsync(string token);

    Task<bool> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO);
}