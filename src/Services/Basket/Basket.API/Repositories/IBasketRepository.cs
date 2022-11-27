using Basket.API.Entities;

namespace Basket.API.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string username, CancellationToken cancellation = default);
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket, CancellationToken cancellation = default);
    Task DeleteBasketAsync(string username, CancellationToken cancellation = default);
}