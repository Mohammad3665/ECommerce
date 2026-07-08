namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithName("نام و نام خانوادگی")
            .MaximumLength(150);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithName("شماره تماس")
            .MaximumLength(11)
            .Matches(@"^0\d{10}$")
            .WithMessage("شماره تماس باید 11 رقمی و با 0 شروع شود.");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithName("آدرس")
            .MaximumLength(500);

        RuleFor(x => x.PostalCode)
            .NotEmpty()
            .WithName("کد پستی")
            .MaximumLength(20);

        RuleFor(x => x.CouponCode)
            .MaximumLength(30)
            .When(x => x.CouponCode is not null);
    }
}
