namespace Nest.Application.Validators;

public class VendorCreateDTOValidator : AbstractValidator<VendorCreateDTO>
{
    public VendorCreateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name cannot be shorter than 3 characters")
            .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(5000).WithMessage("Description cannot be longer than 5000 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email number is required")
            .NotNull().WithMessage("Email number is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .NotNull().WithMessage("Phone number is required")
            .Matches(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$").WithMessage("Phone number is not valid");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address cannot be longer than 200 characters");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image is required")
            .NotEmpty().WithMessage("Image is required")

            .Must(x => CheckFileSize(x)).WithMessage("The size of the image cannot be more than 5 MB")
            .Must(x => CheckFileType(x)).WithMessage("The photo can be in png and jpeg formats");
    }

    private bool CheckFileType(IFormFile image)
    {
        if (image == null)
        {
            return false;
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(image.FileName);
        return allowedExtensions.Contains(fileExtension);
    }

    private bool CheckFileSize(IFormFile image)
    {
        if (image == null)
        {
            return false;
        }

        return image.Length > 50000;
    }
}