namespace Nest.Application.Validators.Auth;

public class VerifyResetTokenDTOValidator : AbstractValidator<VerifyResetTokenDTO>
{
    public VerifyResetTokenDTOValidator()
    {
        RuleFor(x => x.UserId)
           .NotNull().WithMessage("UserId is required")
           .NotEmpty().WithMessage("UserId is required");
        RuleFor(x => x.ResetToken)
            .NotNull().WithMessage("ResetToken is required")
            .NotEmpty().WithMessage("ResetToken is required");
    }
}