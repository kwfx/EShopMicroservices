using System;
using Basket.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Basket.StoreBasket;

public class StoreBasketEnpoint : ICarterModule
{
    public class BasketRequest
    {
        public string Username { get; set; } = "";
        public List<ShoppingCartItem> Items { get; set; } = [];
    }

    public record StoreBasketResponse(string Username);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", (ISender sender, [FromBody] BasketRequest basketRequest) =>
        {
            var cart = basketRequest.Adapt<ShoppingCart>();
            var result = sender.Send(new StoreBasketCommand(cart));
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{cart.Username}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
