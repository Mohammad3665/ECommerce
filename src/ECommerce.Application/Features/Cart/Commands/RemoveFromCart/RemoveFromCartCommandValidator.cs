namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithName("شناسه محصول")
            .GreaterThan(0);
    }
}