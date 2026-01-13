namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto Values) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(request.Values.UserName, cancellationToken);
        if (basket is null)
        {
            return new CheckoutBasketResult(false);
        }
        var checkoutEvent = request.Values.Adapt<BasketCheckoutEvent>();
        checkoutEvent.TotalPrice = basket.TotalPrice;
        await publishEndpoint.Publish(checkoutEvent, cancellationToken);
        await repository.DeleteBasket(request.Values.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}