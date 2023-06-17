Host.CreateDefaultBuilder(args)
.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
})
.ConfigureServices(services =>
{
    services.AddLogging();

    services.AddHostedService<ScopedService>();
})
.Build()
.Run()
;


public class ScopedService(IServiceScopeFactory serviceScopeFactory, ILogger<ScopedService> logger) 
    : ScopedServiceBase(serviceScopeFactory)
{
    public override async Task Scope(IServiceProvider serviceProvider, CancellationToken token)
    {
        logger.Info("into Scope");


    }
}
