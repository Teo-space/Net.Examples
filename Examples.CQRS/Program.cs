global using static GlobalPrint;

using Examples.CQRS;


print("App Runned");

Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddLogging())
    .ConfigureServices(services => services.AddHostedService<Worker>())
    .Build()
    .Run();


public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Info("Worker Execute");

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.Info($"Work!!!");

            await Task.Delay(1000, stoppingToken);



        }
    }
}