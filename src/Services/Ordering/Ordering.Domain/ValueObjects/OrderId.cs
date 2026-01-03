namespace Ordering.Domain.ValueObjects;

public record OrderId
{
    public Guid Value { get; } = default!;

    private OrderId(Guid id)
    {
        Value = id;
    }

    public static OrderId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Order Id could not be empty");
        }
        return new OrderId(id);
    }
}
