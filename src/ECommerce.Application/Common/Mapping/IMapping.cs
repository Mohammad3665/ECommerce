namespace ECommerce.Application.Common.Mapping;

/// <summary>
/// Defines a mapping contract from the current type to a specified destination type.
/// </summary>
/// <typeparam name="TDestination">
/// The destination type to map to.
/// <para>Example: If your entity is Product, use IMapTo&lt;ProductDto&gt;</para>
/// </typeparam>
public interface IMapTo<TDestination>;

/// <summary>
/// Defines a mapping contract from a specified source type to the current type.
/// </summary>
/// <typeparam name="TSource">
/// The source type to map from.
/// <para>Example: If your DTO is ProductDto, use IMapFrom&lt;Product&gt;</para>
/// </typeparam>
public interface IMapFrom<TSource>;