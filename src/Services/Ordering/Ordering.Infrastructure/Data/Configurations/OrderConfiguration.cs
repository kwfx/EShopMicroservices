using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(cId => cId.Value, idDb => OrderId.Of(idDb));
        builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(c => c.OrderId);
        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId).IsRequired();

        builder.ComplexProperty(o => o.OrderName, builder =>
        {
            builder.Property(orderName => orderName.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, builder =>
        {
            builder.Property(ba => ba.AddressLine).HasMaxLength(180).IsRequired();
            builder.Property(ba => ba.City).HasMaxLength(50).IsRequired();
            builder.Property(ba => ba.State).HasMaxLength(50).IsRequired();
            builder.Property(ba => ba.ZipCode).HasMaxLength(5).IsRequired();
            builder.Property(ba => ba.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(ba => ba.LastName).HasMaxLength(50).IsRequired();
            builder.Property(ba => ba.Email).HasMaxLength(50).IsRequired();
        });

        builder.ComplexProperty(o => o.DeliveryAddress, builder =>
        {
            builder.Property(da => da.AddressLine).HasMaxLength(180).IsRequired();
            builder.Property(da => da.City).HasMaxLength(50).IsRequired();
            builder.Property(da => da.State).HasMaxLength(50).IsRequired();
            builder.Property(da => da.ZipCode).HasMaxLength(5).IsRequired();
            builder.Property(da => da.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(da => da.LastName).HasMaxLength(50).IsRequired();
            builder.Property(da => da.Email).HasMaxLength(50).IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, builder =>
        {
            builder.Property(p => p.AddressLine).HasMaxLength(180).IsRequired();
            builder.Property(p => p.CardName).HasMaxLength(50);
            builder.Property(p => p.CardNumber).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Expiration).HasMaxLength(5).IsRequired();
            builder.Property(p => p.CVV).HasMaxLength(3).IsRequired();
            builder.Property(p => p.PaymentMethod);
        });

        builder.Property(o => o.Status).HasConversion(eValue => eValue.ToString(), dValue => (OrderStatus)Enum.Parse(typeof(OrderStatus), dValue));

        builder.Property(o => o.TotalPrice).HasColumnType("decimal(10, 2)");
    }
}