using Discount.API.Models;

namespace Discount.API.Repositories;

public interface IDiscountRepository
{
    Task<Coupon?> GetDiscount(string productName);
    Task<bool> DeleteDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
}