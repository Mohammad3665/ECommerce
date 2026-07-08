using System.Text.Json;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Features.Cart;
using ECommerce.Domain.Common.Error;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace ECommerce.Infrastructure.Common.Services;

public class RedisCartService : ICartService
{
    private readonly IDatabase _redisDb;
    private const string CartKeyPrefix = "cart:";
    private readonly ILogger<RedisCartService> _logger;
    public RedisCartService(IConnectionMultiplexer redis, ILogger<RedisCartService> logger)
    {
        _redisDb = redis.GetDatabase();
        _logger = logger;
    }

    private string GetKey(Guid userId) => $"{CartKeyPrefix}{userId}";

    public async Task<Result> AddItemAsync(Guid userId, RedisCartItemDto itemDto)
    {
        var cartKey = GetKey(userId);
        if (itemDto.ProductId <= 0)
        {
            var error = new Error(
                "Cart.InvalidProductId",
                "آیدی محصول باید بزرگتر از صفر باشد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }
        if (itemDto.Quantity <= 0)
        {
            var error = new Error(
                "Cart.InvalidQuantity",
                "تعداد آیتم باید بیشتر از صفر باشد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }
        var field = itemDto.ProductId.ToString();
        var existingValue = await _redisDb.HashGetAsync(cartKey, field);
        if (existingValue.HasValue)
        {
            var existingItem = JsonSerializer.Deserialize<RedisCartItemDto>((string)existingValue!);
            if (existingItem is null)
            {
                _logger.LogWarning("Failed to deserialize existing cart item for user {UserId}, ProductId: {ProductId}", userId, itemDto.ProductId);
                var error = new Error(
                    "Cart.Failed",
                    "خواندن کالای موجود در سبد خرید ناموفق بود.",
                    ErrorType.Unexpected
                );
                return Result.Failure(error);
            }
            existingItem = existingItem with
            {
                Quantity = existingItem.Quantity + itemDto.Quantity,
                TotalPrice = (existingItem.Quantity + itemDto.Quantity) * existingItem.UnitPrice
            };

            var updatedJson = JsonSerializer.Serialize(existingItem);
            await _redisDb.HashSetAsync(cartKey, field, updatedJson);
        }
        else
        {
            var newItem = itemDto with
            {
                TotalPrice = itemDto.Quantity * itemDto.UnitPrice
            };
            var json = JsonSerializer.Serialize(newItem);
            await _redisDb.HashSetAsync(cartKey, field, json);
        }

        return Result.Success();
    }

    public async Task ClearCart(Guid userId)
    {
        var cartKey = GetKey(userId);
        await _redisDb.KeyDeleteAsync(cartKey);
    }

    public async Task<RedisCartDto> GetCartAsync(Guid userId)
    {
        var cartKey = GetKey(userId);
        var hashEntries = await _redisDb.HashGetAllAsync(cartKey);
        if (hashEntries.Length == 0)
        {
            var now = DateTime.UtcNow;
            var emptyCart = new RedisCartDto(
                UserId: userId,
                Items: new List<RedisCartItemDto>(),
                CreatedAt: now,
                UpdatedAt: now,
                TotalPrice: 0
            );
            return emptyCart;
        }

        var items = new List<RedisCartItemDto>();
        foreach (var entry in hashEntries)
        {
            try
            {
                var json = entry.Value.ToString();
                var item = JsonSerializer.Deserialize<RedisCartItemDto>(json);
                if (item is not null) items.Add(item);
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "Failed to deserialize cart item for user {UserId}, key: {Key}", userId, entry.Name);
                continue;
            }
        }

        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow;

        var totalPrice = items.Sum(c => c.UnitPrice * c.Quantity);

        var cart = new RedisCartDto(
            UserId: userId,
            Items: items,
            CreatedAt: createdAt,
            UpdatedAt: updatedAt,
            TotalPrice: totalPrice
        );
        return cart;
    }

    public async Task<Result> RemoveItemAsync(Guid userId, long ProductId)
    {
        var cartKey = GetKey(userId);
        var field = ProductId.ToString();
        if (ProductId <= 0)
        {
            var error = new Error(
                "Cart.InvalidProductId",
                "آیدی محصول باید بزرگتر از صفر باشد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }
        await _redisDb.HashDeleteAsync(cartKey, field);

        return Result.Success();
    }

    public async Task<Result> UpdateQuantityAsync(Guid userId, long productId, int quantity)
    {
        var cartKey = GetKey(userId);
        if (productId <= 0)
        {
            var error = new Error(
                "Cart.InvalidProductId",
                "آیدی محصول باید بزرگتر از صفر باشد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var field = productId.ToString();
        if (quantity <= 0)
        {
            await _redisDb.HashDeleteAsync(cartKey, field);
            return Result.Success();
        }
        var existingValue = await _redisDb.HashGetAsync(cartKey, field);
        if (existingValue.HasValue)
        {
            var existingItem = JsonSerializer.Deserialize<RedisCartItemDto>((string)existingValue!);
            if (existingItem is null)
            {
                _logger.LogWarning("Failed to deserialize existing cart item for user {UserId}, ProductId: {ProductId}", userId, productId);
                var error = new Error(
                    "Cart.Failed",
                    "خواندن کالای موجود در سبد خرید ناموفق بود.",
                    ErrorType.Unexpected
                );
                return Result.Failure(error);
            }
            existingItem = existingItem with
            {
                Quantity = quantity,
                TotalPrice = quantity * existingItem.UnitPrice
            };
            var json = JsonSerializer.Serialize(existingItem);
            await _redisDb.HashSetAsync(cartKey, field, json);
        }
        return Result.Success();
    }
}