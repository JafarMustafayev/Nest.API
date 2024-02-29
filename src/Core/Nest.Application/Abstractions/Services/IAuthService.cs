namespace Nest.Application.Abstractions.Services;

public interface IAuthService
{
    Task<Token> LoginAsync(LoginDTO loginDTO);

    Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO);

    Task<ResponseDTO> ForgotPasswordAsync(string email);

    Task<ResponseDTO> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO);

    Task<Token> RefreshTokenLoginAsync(string token);

    Task<ResponseDTO> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO);

    Task<ResponseDTO> ResetPasswordAsync(UpdatePasswordDTO updatePasswordDTO);
}