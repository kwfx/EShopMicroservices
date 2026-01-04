
namespace Ordering.Infrastructure;

public static class IniatilizeDataBase
{
    public static List<Customer> Customers => [
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9220")), "Customer 1", "mailexample1@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9221")), "Customer 2", "mailexample2@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9222")), "Customer 3", "mailexample3@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9223")), "Customer 4", "mailexample4@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9224")), "Customer 5", "mailexample5@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9225")), "Customer 6", "mailexample6@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9226")), "Customer 7", "mailexample7@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9227")), "Customer 8", "mailexample8@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9228")), "Customer 9", "mailexample9@eshop.com"),
        Customer.Create(CustomerId.Of(new Guid("4ab75842-a173-462d-b090-1356a54e9229")), "Customer 10", "mailexample10@eshop.com")
    ];

    public static List<Product> Products => [
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9220")), "Product 1", 120),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9221")), "Product 2", 355),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9222")), "Product 3", 60),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9223")), "Product 4", 15),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9224")), "Product 5", 20),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9225")), "Product 6", 875),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9226")), "Product 7", 10),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9227")), "Product 8", 5),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9228")), "Product 9", 6200),
        Product.Create(ProductId.Of(new Guid("4ab75842-b173-462d-b090-1356a54e9229")), "Product 10", 200)
    ];

    public static IEnumerable<Order> Orders
    {
        get
        {
            var billingAddress = Address.Of("Customer", "1", "mailexample1@eshop.com", "45 Av. Dollard", "45213", "Paris", "France");
            var deliveryAddress = Address.Of("Customer", "1", "mailexample1@eshop.com", "15 Av. NewYork", "45213", "Paris", "France");
            var payment = Payment.Of("Card X", "4545 4598 1314 0000", "15/12", "47 Rue des jardins", "787", "0");
            var orderId1 = OrderId.Of(Guid.NewGuid());
            var orderId2 = OrderId.Of(Guid.NewGuid());
            var order1 = Order.Create(orderId1, OrderName.Of("Order 0001"), Customers[0].Id, deliveryAddress, billingAddress, payment);
            order1.AddOrderItem(Products[0].Id, Products[0].Price, 2);
            order1.AddOrderItem(Products[1].Id, Products[1].Price, 10);
            order1.AddOrderItem(Products[2].Id, Products[2].Price, 22);
            var order2 = Order.Create(orderId2, OrderName.Of("Order 0002"), Customers[0].Id, deliveryAddress, billingAddress, payment);
            order2.AddOrderItem(Products[3].Id, Products[3].Price, 32);
            order2.AddOrderItem(Products[4].Id, Products[4].Price, 8);
            order2.AddOrderItem(Products[5].Id, Products[5].Price, 1);
            return [order1, order2];
        }
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
        context.Customers.AddRange(Customers);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProducts(ApplicationDbContext context)
    {
        if (context.Products.Any()) return;
        context.Products.AddRange(Products);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(ApplicationDbContext context)
    {
        if (context.Orders.Any()) return;
        context.Orders.AddRange(Orders);
        await context.SaveChangesAsync();
    }
}
