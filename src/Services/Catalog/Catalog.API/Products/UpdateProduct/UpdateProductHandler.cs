
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductCommand : ICommand<Unit>
    {
        public Guid Id { set; get; }
        public string Name { set; get; } = "";
        public string Description { set; get; } = "";
        public List<string> Categories { set; get; } = [];
        public string ImageFile { get; set; } = "";
        public decimal Price { get; set; } = 0;
    }

    public class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (await session.LoadAsync<Product>(request.Id, cancellationToken) == null)
            {
                throw CustomException.NotFound($"Product With Id: {request.Id} Not Found");
            }
            session.Update(request.Adapt<Product>());
            await session.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}