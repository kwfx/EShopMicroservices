namespace Ordering.API.Endpoints;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{orderId}", async (Guid orderId, OrderDto order, ISender sender) =>
        {
            order = order with { Id = orderId };
            var result = await sender.Send(new UpdateOrderCommand(order));
            return Results.Ok(new UpdateOrderResult(result.IsSuccess));
        })
        .WithName("UpdateOrderEndpoint")
        .Produces<UpdateOrderResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Update order");
    }
}