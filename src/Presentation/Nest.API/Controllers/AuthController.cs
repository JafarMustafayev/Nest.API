namespace Nest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService,
                          IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
    {
        var response = await _authService.LoginAsync(loginDTO);
        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
    {
        var response = await _authService.RegisterAsync(registerDTO);
        return Ok(response);
    }

    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailDTO confirmEmailDTO)
    {
        var response = await _authService.ConfirmEmailAsync(confirmEmailDTO);
        return Ok(response);
    }

    [HttpGet("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
    {
        var response = await _authService.ForgotPasswordAsync(email);
        return Ok(response);
    }

    [HttpPost("RefreshTokenLogin")]
    public async Task<IActionResult> RefreshTokenLogin([FromForm] string refreshToken)
    {
        var response = await _authService.RefreshTokenLoginAsync(refreshToken);
        return Ok(response);
    }

    [HttpPost("VerifyResetToken")]
    public async Task<IActionResult> VerifyResetToken([FromForm] VerifyResetTokenDTO verifyResetTokenDTO)
    {
        var response = await _authService.VerifyResetToken(verifyResetTokenDTO);
        return Ok(response);
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromForm] UpdatePasswordDTO updatePasswordDTO)
    {
        var response = await _authService.ResetPasswordAsync(updatePasswordDTO);
        return Ok(response);
    }

    [HttpGet("LogOut")]
    public async Task<IActionResult> LogOut(string refreshToken)
    {
        var response = await _userService.LogOut(refreshToken);
        return Ok(response);
    }
}