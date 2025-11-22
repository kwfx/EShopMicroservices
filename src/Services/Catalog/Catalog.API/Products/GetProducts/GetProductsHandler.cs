namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery : IQuery<GetProductsQueryResponse> { }

    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        public async Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProductsQueryResponse()
            {
                Products = products.Adapt<List<GetProductQueryResponse>>()
            };
        }
    }
    public class GetProductQueryResponse
    {
        public Guid Id { set; get; }
        public string Name { set; get; } = "";
        public string Description { set; get; } = "";
        public List<string> Categories { set; get; } = [];
        public string ImageFile { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }
    public class GetProductsQueryResponse
    {
        public IEnumerable<GetProductQueryResponse> Products { get; set; } = [];
    }
}