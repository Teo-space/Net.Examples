using Examples.CQRS.MediatR.Commands;
using Examples.CQRS.MediatR.Entities;
using Examples.CQRS.MediatR.MediatR.Behaviors;
using Examples.CQRS.MediatR.MediatR.Decorators;
using Examples.CQRS.MediatR.Query;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


printAppName();


Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddLogging())
    .ConfigureServices(services => services.AddHostedService<Worker>())
    .Build()
    .Run();





public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        serviceProvider = Configure();

        IMediator mediator = serviceProvider.GetRequiredService<IMediator>();

        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await mediator.Send(command);
        logger?.Info($"MeetingMediatRExample: {guid}");


        var query = new GetMeetingQuery(guid);
        var meeting = await mediator.Send(query);
        logger?.Info($"MeetingMediatRExample: {meeting}");


    }



    ServiceProvider serviceProvider;

    ServiceProvider Configure()
    {
        var services = new ServiceCollection();
        //services.AddLogging();
        //services.AddScoped<ILogger, Logger>();
        services.AddLogging();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<List<Meeting>>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.Decorate(typeof(IRequestHandler<,>), typeof(LoggingDecorator<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));


        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}