namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(cId => cId.Value, idDb => ProductId.Of(idDb));
        builder.Property(c => c.Name).HasMaxLength(255).IsRequired();
        builder.Property(c => c.Price).HasColumnType("decimal(10, 2)");
    }
}