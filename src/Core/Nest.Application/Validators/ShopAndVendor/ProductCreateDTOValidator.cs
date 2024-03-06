namespace Nest.Application.Validators.ShopAndVendor;

public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
{
    public ProductCreateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The Product name field is required.")
            .NotNull().WithMessage("The Product name field is required.")
            .MinimumLength(3).WithMessage("The Product Name cannot be shorter than 3 characters")
            .MaximumLength(100).WithMessage("The Product Name cannot be longer than 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The Product description field is required.")
            .NotNull().WithMessage("The Product description field is required.")
            .MinimumLength(3).WithMessage("The Product description  cannot be shorter than 3 characters")
            .MaximumLength(5000).WithMessage("The Product description  cannot be longer than 5000 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("The Product price field is required.")
            .NotNull().WithMessage("The Product price field is required.")
            .GreaterThan(0).WithMessage("The Product price must be greater than 0");

        RuleFor(x => x.Discount)
            .NotEmpty().WithMessage("The Product discount field is required.")
            .NotNull().WithMessage("The Product discount field is required.")
            .GreaterThanOrEqualTo(0).WithMessage("The Product discount must be greater than or equal to 0")
            .LessThanOrEqualTo(100).WithMessage("The Product discount must be less than or equal to 100");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("The Product quantity field is required.")
            .NotNull().WithMessage("The Product quantity field is required.")
            .GreaterThanOrEqualTo(0).WithMessage("The Product quantity must be greater than or equal to 0");

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage("The VendorId field is required.")
            .NotNull().WithMessage("The VendorId field is required.");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("The Product SKU field is required.")
            .NotNull().WithMessage("The Product SKU field is required.");

        RuleFor(x => x.MainImage)
            .NotNull().WithMessage("The Main image field is required.")
            .NotEmpty().WithMessage("The Main image field is required.")

            .Must(x => CheckFileSize(x)).WithMessage("The size of the image cannot be more than 5 MB")
            .Must(x => CheckFileType(x)).WithMessage("The photo can be in png and jpeg formats");

        RuleForEach(x => x.OtherImages)
            .Custom((x, context) =>
            {
                if (x != null)
                {
                    if (!CheckFileSize(x))
                    {
                        context.AddFailure("The size of the image cannot be more than 5 MB");
                    }

                    if (!CheckFileType(x))
                    {
                        context.AddFailure("The photo can be in png and jpeg formats");
                    }
                }
            });
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