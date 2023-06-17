using Microsoft.Extensions.DependencyInjection;
using Common.Extensions.DependencyInjection.Interfaces;



public class LoggerConfiguration : IConfiguration
{
    public void Configure(IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton(typeof(ILogger<>), typeof(LoggerGeneric<>));


    }
}


public static class ExtensionAddLogging
{
    public static IServiceCollection AddLogging(this IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton(typeof(ILogger<>), typeof(LoggerGeneric<>));

        return services;
    }
}
