namespace Nest.Application.Validators;

public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDTOValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters")
            .MinimumLength(5).WithMessage("Username cannot be shorter than 5 characters")
            .NotEmpty().WithMessage("Username is required")
            .NotNull().WithMessage("Username is required")
            .Must(x => BeAvailableUserName(x)).WithMessage("Some characters are useless");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address");

        RuleFor(x => x.PhoneNumber)
            .Must(x => BeAvailablePhoneNumber(x)).WithMessage("Some characters are useless");

        RuleFor(x => x.FullName)
            .NotNull().WithMessage("Fullname is required")
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(100).WithMessage("Fullname cannot be longer than 100 characters")
            .MinimumLength(5).WithMessage("Fullname cannot be shorter than 5 characters")
            .Must(x => BeAvailableFullName(x));

        RuleFor(x => x.ConfirmatedPassword)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password cannot be shorter than 8 characters");
    }

    private bool BeAvailableUserName(string username)
    {
        if (username != null)
        {
            var available = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
            foreach (var item in username)
            {
                if (!available.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private bool BeAvailableFullName(string fullName)
    {
        if (fullName != null)
        {
            var available = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ '";
            foreach (var item in fullName)
            {
                if (!available.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private bool BeAvailablePhoneNumber(string number)
    {
        if (number != null)
        {
            var available = "+-0123456789";
            foreach (var item in number)
            {
                if (!available.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
}