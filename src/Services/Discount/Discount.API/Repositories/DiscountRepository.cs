using Dapper;
using Discount.API.Models;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    private NpgsqlConnection Connection =>
        new(_configuration.GetConnectionString("DiscountDb"));

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Coupon?> GetDiscount(string productName)
    {
        var coupon = await Connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName = @productName", new { productName }
        );
        coupon ??= new()
        {
            Amount = 0,
            ProductName = "No Discount",
            Id = 0
        };
        return coupon;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        return await Connection.ExecuteAsync(
            "DELETE FROM Coupon WHERE ProductName = @productName", new
            {
                productName
            }) > 0;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        return await Connection.ExecuteAsync(
            "INSERT INTO Coupon(ProductName, Description, Amount) VALUES(@ProductName, @Description, @Amount)", new
            {
                coupon.ProductName,
                coupon.Description,
                coupon.Amount
            }) > 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        return await Connection.ExecuteAsync(
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount Where Id = @Id",
            new
            {
                coupon.ProductName,
                coupon.Description,
                coupon.Amount,
                coupon.Id
            }) > 0;
    }
}