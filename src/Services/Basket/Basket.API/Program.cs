using Basket.API.Basket.StoreBasket;
using Basket.API.Core;
using BuildingBlocks.Behaviours;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});
var app = builder.Build();

app.Run();
