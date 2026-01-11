namespace Ordering.API.Endpoints;

public record DeleteOrderRequest(Guid OrderId);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async (Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId));
            return Results.NoContent();
        })
        .WithName("DeleteOrderEndpoint")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Delete order");
    }
}