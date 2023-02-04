using MassTransit;

namespace Net.Examles.Examples.AMQP.MassTransit;


internal class MassTransitConfigurator
{
    public static void Configure(IServiceCollection services)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<MassTransitConsumer>();

            configurator.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddHostedService<MassTransitWorker>();
        //print("AddHostedService MassTransitWorker", ConsoleColor.Green);
    }
}