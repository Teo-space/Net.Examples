using Examples.CQRS.ViaDispatcher.Commands;
using Examples.CQRS.ViaDispatcher.Dispatchers;
using Examples.CQRS.ViaDispatcher.Interfaces;
using Examples.CQRS.ViaDispatcher.Queries;
using FluentValidation;
using Scrutor;


namespace Examples.CQRS.ViaDispatcher;


//LoggerGeneric<T> : LoggerBase, ILogger<T>

public class Example(ILogger logger)// : Handler
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


    public async Task Handle(CancellationToken token)
    {
        serviceProvider = Configure();

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        var queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();

        logger?.Info($"[{this.GetType().Namespace}.{this.GetType().Name}]");


        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await commandDispatcher.Dispatch<CreateMeetingCommand, Guid>(command, CancellationToken.None);
        logger?.Info($"MeetingId: {guid}");



        var query = new GetMeetingQuery(guid);
        var meeting = await queryDispatcher.Dispatch<GetMeetingQuery, Meeting>(query, CancellationToken.None);
        logger?.Info($"{meeting}");
    }


}

