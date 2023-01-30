using System.ComponentModel.DataAnnotations;

namespace Discount.API.Models;

public class Coupon
{
    public int Id { get; set; }
    [StringLength(24)] public string ProductName { get; set; } = default!;
    [StringLength(500)] public string? Description { get; set; }
    public int Amount { get; set; }
}