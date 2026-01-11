using BuildingBlocks.Pagination;

namespace Ordering.API.Endpoints;

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async (ISender sender, int pageIndex = 1, int? pageSize = 20) =>
        {
            var result = await sender.Send(new GetOrdersQuery(pageIndex, pageSize));
            return Results.Ok(result);
        })
        .WithName("GetOrdersEndpoint")
        .Produces<PaginationResult<OrderDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .WithDescription("Get orders by name");
    }
}