using System.Reflection;
using ECommerce.Application.Common.Extensions;
using Mapster;

namespace ECommerce.Application.Common.Mapping;

public class MappingConfig : IRegister
{
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

            var typeConfig = config.NewConfig(type, destination);

            var srcProp = type.GetProperty("EnglishName");
            var destProp = destination.GetProperty("Slug");

            if (srcProp is not null && destProp is not null)
            {
                typeConfig.Map(destProp.Name, 
                   (object src) => ((string)src.GetType().GetProperty("EnglishName")!.GetValue(src, null)!).ToSlug()
                );
            }
        }
    }
}
