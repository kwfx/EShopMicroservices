using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Extensions
{
    public static class AutoMigrateExt
    {
        public static IApplicationBuilder AutoMigrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dataContext = scope.ServiceProvider.GetService<DiscountDbContext>()!;
            dataContext.Database.Migrate();
            return app;
        }
    }
}