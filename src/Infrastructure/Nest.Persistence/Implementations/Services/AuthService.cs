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

    //private readonly ITokenHandler _tokenHandler;;

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

        try
        {
            var roleRes = await _userManager.AddToRoleAsync(user, "Member");

            string errors = "";
            foreach (var err in roleRes.Errors)
            {
                errors += err.Description + "\n";
            }
            throw new InvalidOperationCustomException(errors);
        }
        catch (InvalidOperationCustomException)
        {
            await _userManager.DeleteAsync(user);
            throw new InvalidOperationCustomException("Error adding user to role");
        }
        catch (Exception)
        {
            await _userManager.DeleteAsync(user);
            throw new NotFoundCustomException("Role not found");
        }

        return new ResponseDTO
        {
            Message = "User created successfully",
            Success = true
        };
    }

    public Task<bool> VerifyResetToken(VerifyResetTokenDTO verifyResetTokenDTO)
    {
        throw new NotImplementedException();
    }
}