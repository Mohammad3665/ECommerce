using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Api.Common.Binders;

public class SanitizedStringBinderProvider(IHtmlSanitizerService sanitizer) : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(string))
            return new SanitizedStringBinder(sanitizer);

        return null!;
    }
}