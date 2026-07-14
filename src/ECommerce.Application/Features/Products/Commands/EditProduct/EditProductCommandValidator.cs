namespace ECommerce.Application.Features.Products.Commands.EditProduct;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(x => x.CurrentSlug)
            .NotEmpty()
            .WithName("اسلاگ فعلی")
            .MaximumLength(300);

        RuleFor(x => x.BrandId)
            .NotEmpty()
            .WithName("آیدی برند")
            .GreaterThan(0);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithName("آیدی دسته‌بندی")
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("نام محصول")
            .MaximumLength(100);

        RuleFor(x => x.EnglishName)
            .NotEmpty()
            .WithName("نام انگلیسی")
            .MaximumLength(100)
            .Matches("^[a-zA-Z0-9\\s-_]+$");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithName("توضیحات")
            .MaximumLength(1500);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithName("توضیحات");

        RuleFor(x => x.ShortDescription)
            .NotEmpty()
            .WithName("توضیح کوتاه")
            .MaximumLength(300);

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithName("قیمت")
            .GreaterThan(0);

        RuleFor(x => x.StockQuantity)
            .NotEmpty()
            .WithName("تعداد موجودی")
            .GreaterThan(0);

        RuleFor(x => x.Images)
            .Must(images => images == null || images.Count(img => img.IsMain) <= 1)
            .WithMessage("فقط یک تصویر می‌تواند به عنوان تصویر اصلی (IsMain) انتخاب شود.");
    }
}
