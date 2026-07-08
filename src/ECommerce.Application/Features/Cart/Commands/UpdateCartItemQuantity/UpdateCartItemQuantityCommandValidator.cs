namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandValidator : AbstractValidator<UpdateCartItemQuantityCommand>
{
    public UpdateCartItemQuantityCommandValidator()
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