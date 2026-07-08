namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithName("شناسه محصول")
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .WithName("موجودی")
            .GreaterThan(0);
    }
}