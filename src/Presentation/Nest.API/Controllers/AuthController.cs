namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
    {
        var response = await _authService.LoginAsync(loginDTO);
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
    {
        var response = await _authService.RegisterAsync(registerDTO);
        return Ok(response);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDTO forgotPasswordDTO)
    {
        var response = await _authService.ForgotPasswordAsync(forgotPasswordDTO);
        return Ok(response);
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailDTO confirmEmailDTO)
    {
        var response = await _authService.ConfirmEmailAsync(confirmEmailDTO);
        return Ok(response);
    }

    [HttpPost("refresh-token-login")]
    public async Task<IActionResult> RefreshTokenLogin([FromForm] string token)
    {
        var response = await _authService.RefreshTokenLoginAsync(token);
        return Ok(response);
    }

    [HttpPost("verify-reset-token")]
    public async Task<IActionResult> VerifyResetToken([FromForm] VerifyResetTokenDTO verifyResetTokenDTO)
    {
        var response = await _authService.VerifyResetToken(verifyResetTokenDTO);
        return Ok(response);
    }
}