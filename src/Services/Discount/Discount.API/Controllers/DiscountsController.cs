using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discount.API.Models;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class DiscountsController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountsController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    [HttpGet("{productName}")]
    [Produces(typeof(Coupon))]
    public async Task<IActionResult> GetDiscount(string productName)
        => Ok(await _discountRepository.GetDiscount(productName));
    
    [HttpPost]
    [Produces(typeof(bool))]
    public async Task<IActionResult> CreateDiscount(Coupon coupon)
        => Ok(await _discountRepository.CreateDiscount(coupon));
    
    [HttpPut]
    [Produces(typeof(bool))]
    public async Task<IActionResult> UpdateDiscount(Coupon coupon)
        => Ok(await _discountRepository.UpdateDiscount(coupon));
    
    [HttpDelete("{productName}")]
    [Produces(typeof(bool))]
    public async Task<IActionResult> DeleteDiscount(string productName)
        => Ok(await _discountRepository.DeleteDiscount(productName));
}