namespace Net.Examles.Examples.DependencyInjection.Decorator;


public record Service(ILogger<Service> logger) : IService
{

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger?.Info("Service");
    }


}