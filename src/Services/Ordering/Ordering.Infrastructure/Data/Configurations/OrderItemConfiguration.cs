namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(cId => cId.Value, idDb => OrderItemId.Of(idDb));
        builder.Property(c => c.Quantity).IsRequired();
        builder.Property(c => c.UnitPrice).IsRequired();
        builder.HasOne<Product>().WithMany().HasForeignKey(c => c.ProductId);
        builder.Property(c => c.UnitPrice).HasColumnType("decimal(10, 2)");
        builder.Property(c => c.Quantity).HasColumnType("decimal(10, 2)");
    }
}