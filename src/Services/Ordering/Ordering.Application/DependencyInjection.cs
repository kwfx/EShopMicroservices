using Microsoft.FeatureManagement;

namespace Ordering.Application;

public static class DependencyInjection
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(currentAssembly);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });
        services.AddMessageBroker(configuration, currentAssembly);
        services.AddValidatorsFromAssemblyContaining(typeof(CreateOrderCommandValidator));
        services.AddFeatureManagement();
        return services;
    }
}