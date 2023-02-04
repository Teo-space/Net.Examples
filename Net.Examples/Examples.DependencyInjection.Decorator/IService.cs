namespace Net.Examles.Examples.DependencyInjection.Decorator;


public interface IService
{
    public Task ExecuteAsync(CancellationToken stoppingToken);
}