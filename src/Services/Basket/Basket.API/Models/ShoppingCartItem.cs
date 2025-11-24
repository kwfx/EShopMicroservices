namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Color { get; set; } = "";
    }
}