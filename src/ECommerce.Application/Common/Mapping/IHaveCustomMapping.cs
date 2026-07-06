using Mapster;

namespace ECommerce.Application.Common.Mapping;

/// <summary>
/// Defines a contract for entities or DTOs that require custom object mapping configuration.
/// </summary>
/// <remarks>
/// This interface is designed to centralize and encapsulate mapping logic using Mapster.
/// Implementations provide explicit configuration for object-to-object transformations.
/// Common use cases include custom property mappings, type conversions, and conditional transformations.
/// </remarks>
public interface IHaveCustomMapping
{
    /// <summary>
    /// Configures custom object mapping rules using Mapster's TypeAdapterConfig.
    /// </summary>
    /// <param name="config">
    /// The Mapster configuration instance used to define mapping rules.
    /// <para>Use this parameter to configure mappings between source and destination types.</para>
    /// </param>
    /// <remarks>
    static abstract void ConfigureMapping(TypeAdapterConfig config);
}