using Nest.Application.Consts;

namespace Nest.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ICustomMailService _emailService;
    private readonly IMapper _mapper;

    public AuthService(UserManager<AppUser> userManager,
                       RoleManager<AppRole> roleManager,
                       SignInManager<AppUser> signInManager,
                       ICustomMailService emailService,
                       IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _mapper = mapper;
    }

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

        await _emailService.SendWelcomeEmailAsync(user.Email);

        return new ResponseDTO
        {
            Message = "User created successfully",
            Success = true,
            StatusCode = StatusCodes.Status201Created
        };
    }

    public Task<bool> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO)
    {
        throw new NotImplementedException();
    }
}