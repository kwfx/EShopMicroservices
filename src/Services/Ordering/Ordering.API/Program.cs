using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddWebServices();

var app = builder.Build();
app.UseApiServices();

await app.UseAutoMigrate();

if (app.Environment.IsDevelopment())
{
    await app.UseInitializeData();
}

app.Run();
