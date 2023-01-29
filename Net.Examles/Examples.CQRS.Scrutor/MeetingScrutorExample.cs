using FluentValidation;
using MediatR;
using Net.Examles.Tools.Logger;


namespace Net.Examles.Examples.CQRS.Scrutor;


public record MeetingScrutorExample(ILogger<MeetingScrutorExample> logger) : Handler
{
    List<Meeting> meetings = new();

    ServiceCollection services = new ServiceCollection();

    public async Task Handle(CancellationToken token)
    {
        ConfigureServices(services);

        services.AddScoped<ILogger, Logger>();


        var serviceProvider = services.BuildServiceProvider();

        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var commandHandler = serviceProvider.GetRequiredService<IRequestHandler<CreateMeetingCommand, Guid>>();
        var guid = await commandHandler.Handle(command, CancellationToken.None);
        logger.Info($"guid: {guid}");

        var query = new GetMeetingQuery(guid);
        var queryHandler = serviceProvider.GetRequiredService<IRequestHandler<GetMeetingQuery, Meeting>>();
        var meeting = await queryHandler.Handle(query, CancellationToken.None);
        logger.Info($"meeting: {meeting}");

    }



    void ConfigureServices(ServiceCollection services)
    {
        services.AddLogging();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.Scan(s => s
            .FromCallingAssembly()
            .AddClasses(c => c.AssignableTo(typeof(IRequest<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());


        services.Scan(s => s
        .FromCallingAssembly()
        .AddClasses(c => c.AssignableTo(typeof(IRequest<>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(s => s
        .FromCallingAssembly()
        .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime());
        //foreach(var x in services) print(x);

        services.AddSingleton<List<Meeting>>();

        services.Decorate(typeof(IRequestHandler<,>), typeof(IRequestHandlerDecorator<,>));
    }

}


public record IRequestHandlerDecorator<TRequest, TResponse> (
    ILogger logger,
    IRequestHandler<TRequest, TResponse> handler)

    : IRequestHandler<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        return await handler.Handle(request, cancellationToken);
    }


}