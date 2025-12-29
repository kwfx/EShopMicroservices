using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddCarter(new DependencyContextAssemblyCatalogCustom());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(cfg =>
{
    cfg.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();


builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opts =>
{
    opts.Address = new Uri(builder.Configuration["Grpc:url"]!);
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Default")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler();
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

app.Run();
