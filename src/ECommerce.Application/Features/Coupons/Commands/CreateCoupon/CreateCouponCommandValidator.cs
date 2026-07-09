namespace ECommerce.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithName("کد تخفیف")
            .MaximumLength(30);

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithName("نوع تخفیف")
            .Must(t => t == "Percentage" || t == "FixedAmount")
            .WithMessage("نوع تخفیف باید Percentage یا FixedAmount باشد.");

        RuleFor(x => x.Value)
            .GreaterThan(0)
            .WithName("مقدار تخفیف");

        RuleFor(x => x.Value)
            .LessThanOrEqualTo(100)
            .When(x => x.Type == "Percentage")
            .WithName("مقدار تخفیف")
            .WithMessage("درصد تخفیف نمی‌تواند بیشتر از 100 باشد.");

        RuleFor(x => x.MinOrderAmount)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MinOrderAmount.HasValue)
            .WithName("حداقل مبلغ سفارش");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0)
            .When(x => x.UsageLimit.HasValue)
            .WithName("محدودیت استفاده");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithName("تاریخ شروع");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithName("تاریخ پایان")
            .GreaterThan(x => x.StartDate)
            .WithMessage("تاریخ پایان باید بعد از تاریخ شروع باشد.");
    }
}
