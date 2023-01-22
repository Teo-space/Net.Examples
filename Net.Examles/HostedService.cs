public record HostedService(IServiceScopeFactory serviceScopeFactory, ILogger<ScopedService> logger) 
    
    
    : IHostedService
{


    protected IServiceProvider? serviceProvider { get; private set; }
    public async Task StartAsync(CancellationToken token)
    {
        logger.Info("StartAsync");

        using (var scope = serviceScopeFactory.CreateScope())
        {
            serviceProvider = scope.ServiceProvider;

            await Scope(scope.ServiceProvider, token);
        }
        logger.Info("End");
    }

    public virtual async Task Scope(IServiceProvider? serviceProvider, CancellationToken token)
    {

    }

    public async Task StopAsync(CancellationToken token)
    {
        await Task.Delay(1);
        logger.Info("StopAsync");
    }



    protected T? Get<T>() where T : class => serviceProvider?.GetService<T>();

    protected Task Handle<T>(CancellationToken token) where T : Handler => serviceProvider?.GetService<T>()?.Handle(token);



}