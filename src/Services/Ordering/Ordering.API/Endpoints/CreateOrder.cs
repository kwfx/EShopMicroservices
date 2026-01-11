namespace Ordering.API.Endpoints;

public record CreateOrderResult(Guid OrderId);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (OrderDto order, ISender sender) =>
        {
            var result = await sender.Send(new CreateOrderCommand(order));
            return Results.Created($"/orders/{result.Id}", new CreateOrderResult(result.Id));
        })
        .WithName("CreateOrderEndpoint")
        .Produces<CreateOrderResult>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Create order");
    }
}