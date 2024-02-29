﻿using Microsoft.AspNetCore.Http.HttpResults;
using System.Numerics;

namespace Nest.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ICustomMailService _mailService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public AuthService(UserManager<AppUser> userManager,
                       RoleManager<AppRole> roleManager,
                       SignInManager<AppUser> signInManager,
                       ICustomMailService emailService,
                       IMapper mapper,
                       IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _mailService = emailService;
        _mapper = mapper;
        _config = config;
    }

    public async Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        AppUser user = new();

        user = await _userManager.FindByEmailAsync(registerDTO.Email);

        if (user != null)
        {
            user = await _userManager.FindByNameAsync(registerDTO.UserName);

            if (user != null)
            {
                throw new DuplicateCustomException("User already exists");
            }
        }

        user = _mapper.Map<AppUser>(registerDTO);
        var res = await _userManager.CreateAsync(user, registerDTO.ConfirmatedPassword);
        if (!res.Succeeded)
        {
            string errors = "";
            foreach (var err in res.Errors)
            {
                errors += err.Description + "\n";
            }
            throw new InvalidOperationCustomException(errors);
        }

        if (await _roleManager.FindByNameAsync(UserRoleConsts.User) == null)
        {
            await _roleManager.CreateAsync(new AppRole() { Name = UserRoleConsts.User });
        }

        var roleRes = await _userManager.AddToRoleAsync(user, UserRoleConsts.User);

        if (!roleRes.Succeeded)
        {
            string errors = "";
            foreach (var err in roleRes.Errors)
            {
                errors += err.Description + "\n";
            }
            throw new InvalidOperationCustomException(errors);
        }

        var conToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var conUrl = $"{_config["Urls:Client"]}/auth/confirm-email?email={user.Email.Encode()}&token={conToken.Encode()}";

        await _mailService.SendWelcomeEmailAsync(user.Email, conUrl);

        return new ResponseDTO
        {
            Message = "User successfully created, please check your email",
            Success = true,
            StatusCode = StatusCodes.Status201Created
        };
    }

    public async Task<ResponseDTO> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO)
    {
        var email = confirmEmailDTO.Email.Decode();

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDTO.Token.Decode());

        if (!result.Succeeded)
        {
            throw new InvalidOperationCustomException("Invalid token");
        }

        await _userManager.UpdateSecurityStampAsync(user);

        return new ResponseDTO
        {
            Message = "Email confirmed successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public Task<Token> LoginAsync(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task<Token> RefreshTokenLoginAsync(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetUrl = $"{_config["Urls:Client"]}/auth/reset-password?userId={user.Id.Encode()}&token={token.Encode()}";

        await _mailService.SendEmailForForgotPasswordAsync(user.Email, resetUrl);

        return new ResponseDTO
        {
            Message = "Email sent successfully,please check your email",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<ResponseDTO> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO)
    {
        var user = await _userManager.FindByIdAsync(verifyResetTokenDTO.UserId.Decode());

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        var res = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", verifyResetTokenDTO.ResetToken.Decode());

        if (!res)
        {
            throw new InvalidOperationCustomException("Invalid token");
        }

        return new()
        {
            Errors = null,
            Payload = null,
            Success = res,
            StatusCode = StatusCodes.Status200OK,
            Message = "Token verified successfully"
        };
    }

    public async Task<ResponseDTO> ResetPasswordAsync(UpdatePasswordDTO updatePasswordDTO)
    {
        var user = await _userManager.FindByIdAsync(updatePasswordDTO.UserId.Decode());

        if (user == null)
        {
            throw new NotFoundCustomException("User not found");
        }

        var resVerify = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", updatePasswordDTO.ResetToken.Decode());

        if (!resVerify)
        {
            throw new InvalidOperationCustomException("Invalid token");
        }

        var res = await _userManager.ResetPasswordAsync(user, updatePasswordDTO.ResetToken.Decode(), updatePasswordDTO.NewPassword);

        await _userManager.UpdateSecurityStampAsync(user);

        if (!res.Succeeded)
        {
            string errors = "";
            foreach (var err in res.Errors)
            {
                errors += err.Description + "\n";
            }
            throw new InvalidOperationCustomException(errors);
        }

        return new ResponseDTO
        {
            Message = "Password reset successfully",
            Success = true,
            StatusCode = StatusCodes.Status200OK
        };
    }
}