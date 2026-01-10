namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid orderId) : base("Order", orderId.ToString())
    {

    }
    public OrderNotFoundException(string OrderName) : base("Order", OrderName)
    {

    }
};