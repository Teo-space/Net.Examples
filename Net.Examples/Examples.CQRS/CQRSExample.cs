using Exampes.Utils;
using FluentValidation;
using Net.Examles.Examples.CQRS.Dispatchers;
using Net.Examles.Examples.CQRS.Handlers;
using Net.Examles.Examples.CQRS.Interfaces;
using Net.Examles.Tools.Logger;


namespace Net.Examles.Examples.CQRS;


public record CQRSExample(ILogger logger) : Handler
{
    List<Meeting> meetings = new();

    ServiceCollection services = new ServiceCollection();

    public async Task Handle(CancellationToken token)
    {
        services.AddLogging();
        services.AddScoped<ILogger, Logger>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<List<Meeting>>();

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        //services.AddScoped<ICommandHandler<CreateMeetingCommand, Guid>, CreateMeetingCommand.Handler>();
        services.AddAssignbledTo(Assembly.GetExecutingAssembly(), typeof(ICommandHandler<,>));

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        services.AddAssignbledTo(Assembly.GetExecutingAssembly(), typeof(IQueryHandler<,>));





        var serviceProvider = services.BuildServiceProvider();
        //######################################################################################################
        //######################################################################################################
        //######################################################################################################

        var commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var guid = await commandDispatcher.Dispatch<CreateMeetingCommand, Guid>(command, CancellationToken.None);

        logger?.Info($"MeetingExample: {guid}");


        var queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
        var query = new GetMeetingQuery(guid);
        var meeting = await queryDispatcher.Dispatch<GetMeetingQuery, Meeting>(query, CancellationToken.None);
        logger?.Info($"MeetingExample: {meeting}");


        //var commandDispatcher2 = serviceProvider.GetRequiredService<ICommandDispatcher2>();
        //var command2 = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        //var guid2 = await commandDispatcher2.Dispatch(command2, CancellationToken.None);

    }


}




