using ECommerce.Application.Features.Cart;

namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Provides shopping cart operations using Redis cache.
/// </summary>
/// <remarks>
/// Manages user cart items with Redis for high-performance, distributed caching.
/// All operations are async and thread-safe.
/// </remarks>
public interface ICartService
{
    /// <summary>
    /// Retrieves the user's current cart from Redis cache.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>The user's cart DTO containing all items and totals.</returns>
    /// <remarks>
    /// Returns an empty cart if the user has no items in Redis.
    /// </remarks>
    Task<RedisCartDto> GetCartAsync(Guid userId);

    /// <summary>
    /// Adds a new item to the user's cart.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="itemDto">The product item to add (ProductId, Quantity, Price).</param>
    /// <returns>A Result indicating success or failure with error details.</returns>
    /// <remarks>
    /// If the product already exists in the cart, the quantity is incremented.
    /// </remarks>
    Task<Result> AddItemAsync(Guid userId, RedisCartItemDto itemDto);

    /// <summary>
    /// Removes a specific item from the user's cart.
    /// </summary>
    /// <param name="userid">The unique identifier of the user.</param>
    /// <param name="productId">The ID of the product to remove.</param>
    /// <returns>A Result indicating success or failure.</returns>
    Task<Result> RemoveItemAsync(Guid userId, long productId);

    /// <summary>
    /// Updates the quantity of a specific product in the user's cart.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="productId">The ID of the product to update.</param>
    /// <param name="quantity">The new quantity (must be greater than 0).</param>
    /// <returns>A Result indicating success or failure.</returns>
    /// <remarks>
    /// If quantity is set to 0 or negative, the item is removed from the cart.
    /// </remarks>
    Task<Result> UpdateQuantityAsync(Guid userId, long productId, int quantity);

    /// <summary>
    /// Clears all items from the user's cart.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <remarks>
    /// This operation is irreversible and removes all items from Redis cache.
    /// </remarks>
    Task ClearCart(Guid userId);
}