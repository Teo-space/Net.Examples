global using static GlobalPrint;
using Examples.CQRS.Dispatcher.Commands;
using Examples.CQRS.Dispatcher.Dispatchers;
using Examples.CQRS.Dispatcher.Interfaces;
using Examples.CQRS.Dispatcher.Queries;
using FluentValidation;

printAppName();


Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.AddLogging())
    .ConfigureServices(services => services.AddHostedService<Worker>())
    .Build()
    .Run();


public class Worker(ILogger<Worker> logger) : BackgroundService
{
    ServiceProvider serviceProvider;

    ServiceProvider Configure()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddScoped<ILogger, Common.Logging.Logger.Logger>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<List<Meeting>>();//Вместо репозитория для простоты


        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddAssignbledTo(Assembly.GetExecutingAssembly(), typeof(ICommandHandler<,>));

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddAssignbledTo(Assembly.GetExecutingAssembly(), typeof(IQueryHandler<,>));

        return services.BuildServiceProvider();
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger?.Info($"ExecuteAsync");

        serviceProvider = Configure();

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        var queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();


        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await commandDispatcher.Dispatch<CreateMeetingCommand, Guid>(command, CancellationToken.None);
        logger?.Info($"MeetingId: {guid}");



        var query = new GetMeetingQuery(guid);
        var meeting = await queryDispatcher.Dispatch<GetMeetingQuery, Meeting>(query, CancellationToken.None);
        logger?.Info($"{meeting}");


    }
}

