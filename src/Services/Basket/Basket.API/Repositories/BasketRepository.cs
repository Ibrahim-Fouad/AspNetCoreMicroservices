using System.Text.Json;
using Basket.API.Entities;
using Basket.API.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;
    private readonly DiscountGrpcService _discountGrpcService;

    public BasketRepository(IDistributedCache redisCache, DiscountGrpcService discountGrpcService)
    {
        _redisCache = redisCache;
        _discountGrpcService = discountGrpcService;
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
        var result = await _discountGrpcService.GetDiscountsAsync(basket.Items.Select(x => x.ProductName));
        for (var i = 0; i < basket.Items.Count; i++)
        {
            if (result[i].Id is 0) continue;
            basket.Items[i].Price -= Math.Abs(result[i].Amount);
        }

        await _redisCache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket), cancellation);
        return await GetBasketAsync(basket.Username, cancellation);
    }

    public Task DeleteBasketAsync(string username, CancellationToken cancellation = default)
    {
        return _redisCache.RemoveAsync(username, cancellation);
    }
}