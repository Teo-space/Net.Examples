using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/// <summary>
/// Базовый класс для Scoped Сервиса который будет хостом
/// </summary>
/// <param name="serviceScopeFactory"></param>
/// <param name="logger"></param>
public class ScopedServiceBase(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    protected IServiceProvider? serviceProvider { get; private set; }
    protected T? Get<T>() where T : class => serviceProvider?.GetService<T>();

    public async Task StartAsync(CancellationToken token)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            serviceProvider = scope.ServiceProvider;

            await Scope(scope.ServiceProvider, token);
        }
    }


    public virtual async Task Scope(IServiceProvider? serviceProvider, CancellationToken token)
    {

    }

    public async Task StopAsync(CancellationToken token)
    {


    }

}

