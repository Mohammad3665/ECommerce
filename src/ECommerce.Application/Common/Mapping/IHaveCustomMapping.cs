using Mapster;

namespace ECommerce.Application.Common.Mapping;

public interface IHaveCustomMapping
{
    static abstract void ConfigureMapping(TypeAdapterConfig config);
}