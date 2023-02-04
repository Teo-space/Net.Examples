using FluentValidation;
using MediatR;
using Net.Examles.Tools.Logger;

namespace Net.Examles.Examples.CQRS.MediarR;


public record MeetingMediatRExample(ILogger logger) : Handler
{

    List<Meeting> meetings = new();

    ServiceCollection services = new ServiceCollection();

    public async Task Handle(CancellationToken token)
    {
        services.AddLogging();
        services.AddScoped<ILogger, Logger>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<List<Meeting>>();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddSingleton<Service>();
        services.AddScoped<Service>();

        services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingDecorator<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


        var serviceProvider = services.BuildServiceProvider();

        IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await mediator.Send(command);
        logger?.Info($"MeetingMediatRExample: {guid}");

        var query = new GetMeetingQuery(guid);
        var meeting = await mediator.Send(query);
        logger?.Info($"MeetingMediatRExample: {meeting}");

    }


}



public record Service(ILogger logger, IMediator mediator)
{
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.Info("Service: Log something here.");

        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await mediator.Send(command);
        logger.Info($"Service: {guid}");

        var query = new GetMeetingQuery(guid);
        var meeting = await mediator.Send(query);
        logger.Info($"Service: {meeting}");
    }

}


record LoggingDecorator<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> handler,
        ILogger logger)

    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        logger.Info("LoggingDecorator");

        return handler.Handle(request, cancellationToken);
    }
}


record LoggingBehavior<TRequest, TResponse>(ILogger logger)

    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            logger.Info($"LoggingBehavior.  Before execution for {typeof(TRequest).Name}");

            return await next();
        }
        finally
        {
            logger.Info($"LoggingBehavior.  After execution for {typeof(TRequest).Name}");
        }
    }
}


