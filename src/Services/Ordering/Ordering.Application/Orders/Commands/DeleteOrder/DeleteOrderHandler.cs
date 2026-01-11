
namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var dbOrder = await dbContext.Orders.FindAsync(OrderId.Of(request.Id), cancellationToken);
        if (dbOrder is null)
        {
            throw new OrderNotFoundException(request.Id);
        }
        dbContext.Orders.Remove(dbOrder);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(IsSuccess: true);
    }
}