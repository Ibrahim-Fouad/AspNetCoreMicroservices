using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetBasketAsync(string username, CancellationToken cancellationToken)
        => Ok(await _basketRepository.GetBasketAsync(username, cancellationToken) ?? new ShoppingCart(username));


    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    [HttpPost]
    public async Task<IActionResult> UpdateBasketAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
        => Ok(await _basketRepository.UpdateBasketAsync(shoppingCart, cancellationToken));


    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    [HttpDelete("{username}")]
    public async Task<IActionResult> DeleteBasketAsync(string username, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasketAsync(username, cancellationToken);
        return Ok();
    }
}