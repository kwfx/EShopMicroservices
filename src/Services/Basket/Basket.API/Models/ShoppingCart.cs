namespace Basket.API.Models
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Select(i => i.UnitPrice * i.Quantity).Sum();
        public ShoppingCart(string username)
        {
            Username = username;
        }

        public ShoppingCart()
        {
        }
    }
}