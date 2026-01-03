namespace Ordering.Domain.ValueObjects;

public record OrderItemId
{
    public Guid Value { get; } = default!;

    private OrderItemId(Guid id)
    {
        Value = id;
    }

    public static OrderItemId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("OrderItem Id could not be empty");
        }
        return new OrderItemId(id);
    }
}
