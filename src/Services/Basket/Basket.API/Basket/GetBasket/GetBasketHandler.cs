
using Discount.Grpc;

namespace Basket.API.Basket.GetBasket;

public class GetBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountClient) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var cart = await basketRepository.GetBasket(request.Username, cancellationToken);
        foreach (var item in cart.Items)
        {
            var coupon = await discountClient.GetDiscountAsync(new() { ProductName = item.ProductName });
            item.UnitPrice -= coupon?.Amount ?? 0;
        }
        return new GetBasketResult(cart);
    }
}

public record GetBasketQuery(string Username) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart Cart);