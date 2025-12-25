using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDbContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Iphone 16", Description = "Iphone 20% Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Iphone 16 Pro", Description = "Iphone 12% Discount", Amount = 320 },
            new Coupon { Id = 3, ProductName = "Iphone 17", Description = "Iphone 20% Discount", Amount = 400 },
            new Coupon { Id = 4, ProductName = "Iphone 17 Pro", Description = "Iphone 20% Discount", Amount = 250 }
        );
    }
}