namespace Catalog.API.Products.GetProductByCategoryEndpoint
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("/products/category/{category}", (ISender sender, string category) =>
            {
                return sender.Send(new GetProductByCategoryQuery() { Category = category });
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
    public class GetProductResponse
    {
        public Guid Id { set; get; }
        public string Name { set; get; } = "";
        public string Description { set; get; } = "";
        public List<string> Categories { set; get; } = [];
        public string ImageFile { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }
}
