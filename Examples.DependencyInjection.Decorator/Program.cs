ServiceCollection services = new ServiceCollection();
{
    services.AddLogging();
    services.AddSingleton<IService, Service>();
    services.Decorate<IService, ServiceDecorator>();
}

var serviceProvider = services.BuildServiceProvider();

var service = serviceProvider.GetRequiredService<IService>();

await service.ExecuteAsync(CancellationToken.None);



public interface IService
{
    public Task ExecuteAsync(CancellationToken stoppingToken);
}


class Service(ILogger<Service> logger) : IService
{
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger?.Info("Executed");
    }
}


class ServiceDecorator(ILogger<ServiceDecorator> logger, IService service) : IService
{
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger?.Info("Execute Start");

        await service.ExecuteAsync(stoppingToken);

        logger?.Info("Execute End");


    }
}

