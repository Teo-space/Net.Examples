namespace Net.Examles.Examples.DependencyInjection.Decorator;


public record ServiceDecoratorWithScrutorExample(ILogger<ServiceDecoratorWithScrutorExample> logger) : Handler
{
    ServiceCollection services = new ServiceCollection();

    public async Task Handle(CancellationToken token)
    {
        services.AddLogging();
        //services.AddSingleton<List<Meeting>>();
        services.AddSingleton<IService, Service>();
        services.Decorate<IService, ServiceDecorator>();


        var serviceProvider = services.BuildServiceProvider();
        var service = serviceProvider.GetRequiredService<IService>();

        await service.ExecuteAsync(CancellationToken.None);


    }


}


