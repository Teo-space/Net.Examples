using MassTransit;


namespace Net.Examles.Examples.AMQP.MassTransit;


public class MassTransitConsumer : IConsumer<MassTransitMessage>
{
    readonly ILogger<MassTransitConsumer> logger;
    public MassTransitConsumer(ILogger<MassTransitConsumer> logger)
    {

        this.logger = logger;

    }

    public async Task Consume(ConsumeContext<MassTransitMessage> context)
    {
        //logger.LogInformation("GettingStartedConsumerReceived Text: {Text}", context.Message.Value);
        //return Task.CompletedTask;
    }


}