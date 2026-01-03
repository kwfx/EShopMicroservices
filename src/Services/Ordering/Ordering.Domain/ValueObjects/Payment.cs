namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string AddressLine { get; } = default!;
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;

    protected Payment()
    {

    }

    private Payment(string? cardname, string cardnumber, string expiration, string addressline, string cvv, string paymentmethod)
    {
        CardName = cardname;
        CardNumber = cardnumber;
        Expiration = expiration;
        AddressLine = addressline;
        CVV = cvv;
        PaymentMethod = paymentmethod;
    }

    public static Payment Of(string? cardname, string cardnumber, string expiration, string addressline, string cvv, string paymentmethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardnumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressline);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        return new Payment(cardname, cardnumber, expiration, addressline, cvv, paymentmethod);
    }
}