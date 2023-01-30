using Discount.Grpc.Models;

namespace Discount.Grpc.Repositories;

public interface IDiscountRepository
{
    Task<Coupon?> GetDiscount(string productName);
    Task<bool> DeleteDiscount(string productName);
    Task<int> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
}