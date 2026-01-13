
namespace Basket.API.Basket.CheckoutBasket;

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("basket/{username}/checkout", async (string username, CheckoutBasketDto values, ISender sender) =>
        {
            values.UserName = username;
            var result = await sender.Send(new CheckoutBasketCommand(values));
            return Results.Ok(result);
        })
        .WithName("CheckoutBasketEndpoint")
        .Produces<CheckoutBasketResult>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status402PaymentRequired);
    }
}