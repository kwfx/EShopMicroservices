
using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(request.Username);
        return new GetBasketResult(cart);
    }
}

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);