﻿namespace Nest.API.Controllers;

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

    [HttpPost("confirmemail")]
    public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailDTO confirmEmailDTO)
    {
        var response = await _authService.ConfirmEmailAsync(confirmEmailDTO);
        return Ok(response);
    }

    [HttpGet("forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
    {
        var response = await _authService.ForgotPasswordAsync(email);
        return Ok(response);
    }

    [HttpPost("refreshtokenlogin")]
    public async Task<IActionResult> RefreshTokenLogin([FromForm] string refreshToken)
    {
        var response = await _authService.RefreshTokenLoginAsync(refreshToken);
        return Ok(response);
    }

    [HttpPost("verifyresettoken")]
    public async Task<IActionResult> VerifyResetToken([FromForm] VerifyResetTokenDTO verifyResetTokenDTO)
    {
        var response = await _authService.VerifyResetToken(verifyResetTokenDTO);
        return Ok(response);
    }

    [HttpPost("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromForm] UpdatePasswordDTO updatePasswordDTO)
    {
        var response = await _authService.ResetPasswordAsync(updatePasswordDTO);
        return Ok(response);
    }
}