using System.Text.Json;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string username, CancellationToken cancellation = default)
    {
        var basket = await _redisCache.GetStringAsync(username, cancellation);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonSerializer.Deserialize<ShoppingCart?>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellation = default)
    {
        await _redisCache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellation);
        return await GetBasketAsync(basket.Username, cancellation);
    }

    public Task DeleteBasketAsync(string username, CancellationToken cancellation = default)
    {
        return _redisCache.RemoveAsync(username, cancellation);
    }
}