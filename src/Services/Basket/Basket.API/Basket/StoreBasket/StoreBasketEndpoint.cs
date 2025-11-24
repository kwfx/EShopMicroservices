using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket;

public class StoreBasketEnpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("basket", async (ISender sender, BasketRequest basketRequest) =>
        {
            var cart = basketRequest.Adapt<ShoppingCart>();
            var result = await sender.Send(new StoreBasketCommand(cart));
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"basket/{cart.Username}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}

public class BasketRequest
{
    public string Username { get; set; } = "";
    public List<ShoppingCartItem> Items { get; set; } = [];
}

public record StoreBasketResponse(string Username);
