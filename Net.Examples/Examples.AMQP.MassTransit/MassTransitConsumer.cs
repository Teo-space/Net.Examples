using MassTransit;


namespace Net.Examles.Examples.AMQP.MassTransit;


public record MassTransitConsumer(ILogger<MassTransitConsumer> logger)

    : IConsumer<MassTransitMessage>
{
    public async Task Consume(ConsumeContext<MassTransitMessage> context)
    {
        logger.Info("GettingStartedConsumerReceived Text: {Text}", context.Message.Value);
        //return Task.CompletedTask;
    }

}

