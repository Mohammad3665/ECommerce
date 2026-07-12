using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Api.Common.Binders;

public class SanitizedStringBinder(IHtmlSanitizerService sanitizer) : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
        var clean = sanitizer.Clean(value);
        bindingContext.Result = ModelBindingResult.Success(clean);
        return Task.CompletedTask;
    }
}