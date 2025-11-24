
namespace Basket.API.Basket.GetBasket;

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("basket/{username}", async (ISender sender, string username) =>
        {
            var result = await sender.Send(new GetBasketRequest(username).Adapt<GetBasketQuery>());
            return result.Adapt<GetBasketResponse>();
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public record GetBasketRequest(string Username);

public record GetBasketResponse(ShoppingCart Cart);