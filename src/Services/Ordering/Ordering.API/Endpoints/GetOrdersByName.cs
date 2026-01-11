namespace Ordering.API.Endpoints;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByNameQuery(orderName));
            return Results.Ok(new GetOrdersByNameResult(result.Orders));
        })
        .WithName("GetOrdersByNameEndpoint")
        .Produces<GetOrdersByNameResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get orders by name");
    }
}