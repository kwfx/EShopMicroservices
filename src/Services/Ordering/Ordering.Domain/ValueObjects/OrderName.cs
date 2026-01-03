namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultNameLength = 5;

    public string Value { get; } = default!;

    private OrderName(string name)
    {
        Value = name;
    }

    public static OrderName Of(string name)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(name.Length, DefaultNameLength);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return new OrderName(name);
    }
}
