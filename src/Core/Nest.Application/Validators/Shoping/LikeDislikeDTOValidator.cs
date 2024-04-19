namespace Nest.Application.Validators.Shoping;

public class LikeDislikeDTOValidator : AbstractValidator<LikeAndUnLikeDTO>
{
    public LikeDislikeDTOValidator()
    {
        RuleFor(x => x.UserRefreshToken)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MaximumLength(100);
    }
}