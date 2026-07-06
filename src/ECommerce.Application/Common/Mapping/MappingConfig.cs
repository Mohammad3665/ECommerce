using ECommerce.Application.Common.Extensions;
using Mapster;

namespace ECommerce.Application.Common.Mapping;

/// <summary>
/// Configures Mapster mappings for the application.
/// </summary>
/// <remarks>
/// Automatically registers mappings for types implementing IMapFrom or IMapTo.
/// Supports custom mapping configuration via IHaveCustomMapping.
/// </remarks>
public class MappingConfig : IRegister
{
    /// <summary>
    /// Registers all mapping configurations.
    /// </summary>
    /// <param name="config">The Mapster configuration instance.</param>
    public void Register(TypeAdapterConfig config)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var mapFromTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

        foreach (var type in mapFromTypes)
        {
            var source = type.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>))
                .GetGenericArguments()[0];
            if (typeof(IHaveCustomMapping).IsAssignableFrom(type))
            {
                var method = type.GetMethod(nameof(IHaveCustomMapping.ConfigureMapping), BindingFlags.Public | BindingFlags.Static);
                method?.Invoke(null, [config]);
            }
            else
                config.NewConfig(source, type);
        }

        var mapToTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToList();

        foreach (var type in mapToTypes)
        {
            var destination = type.GetInterfaces()
                .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>))
                .GetGenericArguments()[0];

            TypeAdapterSetter? typeConfig = null;

            if (typeof(IHaveCustomMapping).IsAssignableFrom(type))
            {
                var method = type.GetMethod(nameof(IHaveCustomMapping.ConfigureMapping), BindingFlags.Public | BindingFlags.Static);
                method?.Invoke(null, [config]);
            }
            else
                typeConfig = config.NewConfig(type, destination);

            var srcProp = type.GetProperty("EnglishName");
            var destProp = destination.GetProperty("Slug");

            if (srcProp is not null && destProp is not null)
            {
                typeConfig ??= config.NewConfig(type, destination);
                typeConfig.Map(destProp.Name,
                   (object src) => ((string)src.GetType().GetProperty("EnglishName")!.GetValue(src, null)!).ToSlug()
                );
            }
        }
    }
}
