using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Net.Examles.Examples.DependencyInjection;


public record DependencyInjectionUsingHostExample(ILogger<DependencyInjectionUsingHostExample> logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
         await Host.CreateDefaultBuilder()
            .ConfigureServices((x, services) =>
            {
                services.AddLogging();

                services.AddSingleton<ConcurrentDictionary<Guid, NicePeople>>();

                services.AddSingleton<IGenericRepository<NicePeople, Guid>, NicePeopleGenericRepository>();

                services.AddScoped<NicePeopleExample>();
            })
            .Build()
            .RunAsync();
    }


}
