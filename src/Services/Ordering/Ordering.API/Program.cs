using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices()
                .AddInfrastructureServices(builder.Configuration)
                .AddWebServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

await app.UseInitializeData();

app.Run();
