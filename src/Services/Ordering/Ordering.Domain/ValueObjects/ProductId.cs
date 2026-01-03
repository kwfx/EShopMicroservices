namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; } = default!;

    private ProductId(Guid id)
    {
        Value = id;
    }

    public static ProductId Of(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("Product Id could not be empty");
        }
        return new ProductId(id);
    }
}
