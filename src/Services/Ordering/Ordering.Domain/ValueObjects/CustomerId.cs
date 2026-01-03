namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; } = default!;

    private CustomerId(Guid id)
    {
        Value = id;
    }

    public static CustomerId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Customer Id could not be empty");
        }
        return new CustomerId(id);
    }
}
