using MassTransit;


namespace Net.Examles.Examples.AMQP.MassTransit;


internal class MassTransitWorker : BackgroundService
{
    readonly IBus bus;
    public MassTransitWorker(IBus bus)
    {
        this.bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await bus.Publish(new MassTransitMessage { Value = $"The time is {DateTimeOffset.Now}" }, stoppingToken);

            await Task.Delay(1000, stoppingToken);
        }

    }
}