namespace Net.Examles.Tools.Logger;

internal class LoggerConfiguration : IConfiguration
{
    public void Configure(IServiceCollection services)
    {
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton(typeof(ILogger<>), typeof(LoggerGeneric<>));


    }
}
