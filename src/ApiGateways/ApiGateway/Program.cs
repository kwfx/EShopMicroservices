
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixedWindowPolicy", cfg =>
    {
        cfg.PermitLimit = 5;
        cfg.Window = TimeSpan.FromSeconds(10);
    });
});

var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();

app.MapGet("/", () => "Hello World!");

app.Run();
