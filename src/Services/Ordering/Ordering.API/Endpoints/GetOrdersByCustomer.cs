namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/{customerId}/orders", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
            return Results.Ok(new GetOrdersByCustomerResult(result.Orders));
        })
        .WithName("GetOrdersByCustomerEndpoint")
        .Produces<GetOrdersByCustomerResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get orders by name");
    }
}