namespace Net.Examles.Examples.DependencyInjection.Decorator;


public record ServiceDecorator(ILogger<ServiceDecorator> logger, IService service)
    : IService
{
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger?.Info("ServiceDecorator");

        await service.ExecuteAsync(stoppingToken);

        logger?.Info("ServiceDecorator");


    }


}