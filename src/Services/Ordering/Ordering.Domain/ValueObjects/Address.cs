namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string Email { get; } = default!;
    public string AddressLine { get; } = default!;
    public string ZipCode { get; } = default!;
    public string City { get; } = default!;
    public string State { get; } = default!;

    protected Address()
    {
    }

    private Address(string firstname, string lastname, string email, string addressline, string zipcode, string city, string state)
    {
        FirstName = firstname;
        LastName = lastname;
        Email = email;
        AddressLine = addressline;
        ZipCode = zipcode;
        City = city;
        State = state;
    }

    public static Address Of(string firstname, string lastname, string email, string addressline, string zipcode, string city, string state)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressline);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipcode);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        return new Address(firstname, lastname, email, addressline, zipcode, city, state);
    }
}