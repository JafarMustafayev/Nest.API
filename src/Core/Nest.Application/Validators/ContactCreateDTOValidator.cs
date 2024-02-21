namespace Nest.Application.Validators;

public class ContactCreateDTOValidator : AbstractValidator<ContactCreateDTO>
{
    public ContactCreateDTOValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("max 100 dene ola biler ");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("email adresi yaz ")
            .MaximumLength(100)
            .WithMessage("max 100 dene ola biler ");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("subject is required")
            .MaximumLength(100)
            .WithMessage("max 100 dene ola biler ");

        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Message is required")
            .MaximumLength(2500)
            .WithMessage("max 100 dene ola biler ");
    }
}