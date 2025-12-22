
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache, ILogger<CachedBasketRepository> logger) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string username, CancellationToken token = default)
    {
        var res = await repository.DeleteBasket(username, token);
        await cache.RemoveAsync($"cart-{username}", token);
        return res;
    }

    public async Task<ShoppingCart> GetBasket(string username, CancellationToken token = default)
    {
        ShoppingCart cart;
        var cartBytes = await cache.GetAsync($"cart-{username}", token);
        if (cartBytes?.Length > 0)
        {
            logger.LogInformation("Getting Cart from cache ....");
            cart = JsonSerializer.Deserialize<ShoppingCart>(cartBytes)!;
            return cart;
        }
        cart = await repository.GetBasket(username, token);
        await cache.SetAsync($"cart-{username}", JsonSerializer.SerializeToUtf8Bytes(cart), token);
        return cart;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart cart, CancellationToken token = default)
    {
        await repository.StoreBasket(cart, token);
        await cache.SetAsync($"cart-{cart.Username}", JsonSerializer.SerializeToUtf8Bytes(cart), token);
        return cart;
    }
}