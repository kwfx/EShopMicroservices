using Basket.API.Models;

namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string username, CancellationToken token = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token = default);
        Task<bool> DeleteBasket(string username, CancellationToken token = default);
    }
}