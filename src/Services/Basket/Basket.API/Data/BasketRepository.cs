namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string username, CancellationToken token = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(username, token)
                ?? throw new NotFoundException("ShoppingCart", username);
            session.Delete(cart);
            await session.SaveChangesAsync(token);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string username, CancellationToken token = default)
        {
            var cart = await session.LoadAsync<ShoppingCart>(username, token) ??
                throw new NotFoundException("ShoppingCart", username);
            return cart;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token = default)
        {
            session.Store(cart);
            await session.SaveChangesAsync(token);
            return cart;
        }
    }
}