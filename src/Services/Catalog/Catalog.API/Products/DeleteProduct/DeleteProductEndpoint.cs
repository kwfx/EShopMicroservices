
namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{productId}", async (ISender sender, Guid productId) =>
            {
                await sender.Send(new DeleteProductCommand(productId));
                return Results.NoContent();
            })
            .WithName("DeleteProduct")
            .Produces<Unit>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}