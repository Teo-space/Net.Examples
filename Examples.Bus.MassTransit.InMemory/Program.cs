using MassTransit;

printAppName();


Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddLogging())
    .ConfigureServices(services =>
    {
        print("AddMassTransit");
        services.AddMassTransit(configurator =>
        {
            print("AddConsumer<Consumer>()");
            configurator.AddConsumer<Consumer>();

            print("UsingInMemory");
            configurator.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .ConfigureServices(services => services.AddHostedService<Producer>())
    .Build()
    .Run();


public class Message
{
    public string Value { get; set; }
}



public class Producer(ILogger<Producer> logger, IBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Info("Producer");

        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Publish(new Message { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }
    }
}


public class Consumer(ILogger<Consumer> logger) : IConsumer<Message>
{
    public async Task Consume(ConsumeContext<Message> context)
    {
        logger.Info($"Consume: ", context.Message.Value);
        //return Task.CompletedTask;


    }

}

