namespace Nest.Application.Validators.Auth;

public class UpdatePasswordDTOValidator : AbstractValidator<UpdatePasswordDTO>
{
    public UpdatePasswordDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .NotNull().WithMessage("UserId is required");
        ;
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .NotNull().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password cannot be shorter than 8 characters");

        RuleFor(x => x.ResetToken)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required");

    }
}