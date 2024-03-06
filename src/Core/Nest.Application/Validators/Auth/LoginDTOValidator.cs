namespace Nest.Application.Validators.Auth;

public class LoginDTOValidator : AbstractValidator<LoginDTO>
{
    public LoginDTOValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or username is required")
            .NotNull().WithMessage("Email or username is required");

        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password cannot be shorter than 8 characters")
            .MaximumLength(64).WithMessage("Pasword cannot be longer than 64 characters");
    }
}