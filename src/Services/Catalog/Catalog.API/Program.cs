using Catalog.API.Core;
using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
});

var app = builder.Build();

app.MapCarter();

app.Run();

