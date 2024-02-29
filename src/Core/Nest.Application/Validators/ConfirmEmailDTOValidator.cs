namespace Nest.Application.Validators;

public class ConfirmEmailDTOValidator : AbstractValidator<ConfirmEmailDTO>
{
    public ConfirmEmailDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .NotNull().WithMessage("Email is required");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required")
            .NotNull().WithMessage("Token is required");
    }
}