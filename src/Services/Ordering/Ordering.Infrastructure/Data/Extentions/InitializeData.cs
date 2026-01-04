using Microsoft.Extensions.Hosting;

namespace Ordering.Infrastructure.Data.Extentions;

public static class InitializeData
{
    public static async Task<WebApplication> UseInitializeData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        if (app.Environment.IsDevelopment())
        {
            await SeedData(context);
        }
        return app;
    }

    public static async Task SeedData(ApplicationDbContext context)
    {
        await SeedCustomers(context);
        await SeedProducts(context);
        await SeedOrders(context);
    }

    private static async Task SeedCustomers(ApplicationDbContext context)
    {
        if (context.Customers.Any()) return;
        context.Customers.AddRange(InitialData.Customers);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProducts(ApplicationDbContext context)
    {
        if (context.Products.Any()) return;
        context.Products.AddRange(InitialData.Products);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(ApplicationDbContext context)
    {
        if (context.Orders.Any()) return;
        context.Orders.AddRange(InitialData.Orders);
        await context.SaveChangesAsync();
    }
}