using System.Collections.Concurrent;

namespace Net.Examles.Examples.DependencyInjection;


public record DependencyInjectionServiceCollectionExample(ILogger<DependencyInjectionServiceCollectionExample> logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
        var services = new ServiceCollection();

        services.AddLogging();

        services.AddSingleton<ConcurrentDictionary<Guid, NicePeople>>();
        services.AddSingleton<IGenericRepository<NicePeople, Guid>, NicePeopleGenericRepository>();

        services.AddScoped<NicePeopleExample>();
        //Run auto
        //services.AddHostedService<NicePeopleWorker>();

        var serviceProvider = services.BuildServiceProvider();

        var worker = serviceProvider.GetService<NicePeopleExample>();

        await worker.Handle(CancellationToken.None);


    }


}

