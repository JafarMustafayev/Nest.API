namespace Nest.Application.Validators.ShopAndVendor;

public class ProductUpdateDTOValidator : AbstractValidator<ProductUpdateDTO>
{
    public ProductUpdateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required")
            .NotNull().WithMessage("Product name is required")
            .MinimumLength(3).WithMessage("Product Name cannot be shorter than 3 characters")
            .MaximumLength(100).WithMessage("Product Name cannot be longer than 100 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required")
            .NotNull().WithMessage("Product description is required")
            .MinimumLength(3).WithMessage("Product description  cannot be shorter than 3 characters")
            .MaximumLength(5000).WithMessage("Product description  cannot be longer than 5000 characters");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product price is required")
            .NotNull().WithMessage("Product price is required")
            .GreaterThan(0).WithMessage("Product price must be greater than 0");

        RuleFor(x => x.Discount)
            .NotEmpty().WithMessage("Product discount is required")
            .NotNull().WithMessage("Product discount is required")
            .GreaterThanOrEqualTo(0).WithMessage("Product discount must be greater than or equal to 0")
            .LessThanOrEqualTo(99).WithMessage("The Product discount must be less than or equal to 99");

        RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Product quantity is required")
            .NotNull().WithMessage("Product quantity is required")
            .GreaterThanOrEqualTo(0).WithMessage("Product quantity must be greater than or equal to 0");

        RuleFor(x => x.VendorId)
            .NotEmpty().WithMessage("Vendor is required")
            .NotNull().WithMessage("Vendor is required");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("Product SKU is required")
            .NotNull().WithMessage("Product SKU is required");

        RuleFor(x => x.MainImage)
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