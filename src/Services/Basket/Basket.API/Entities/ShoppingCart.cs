namespace Basket.API.Entities;

public class ShoppingCart
{
    public string Username { get; set; } = null!;
    public IList<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice => Items.Select(itm => itm.Price * itm.Quantity).Sum();

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public ShoppingCart()
    {
    }
}