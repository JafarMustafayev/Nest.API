namespace Nest.Application.Validators;

public class ForgotPasswordDTOValidator : AbstractValidator<ForgotPasswordDTO>
{
    public ForgotPasswordDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}