namespace Net.Examles.Examples.CQRS.Manual;


public record MeetingManualExample(ILogger logger) : Handler
{

    List<Meeting> meetings = new();

    public async Task Handle(CancellationToken token)
    {
        var command = new CreateMeetingCommand("Tea ceremony", DateTime.Now);
        var commandHandler = new CreateMeetingCommand.Handler(meetings);

        var guid = await commandHandler.Handle(command, CancellationToken.None);
        logger.Info($"guid: {guid}");

        var query = new GetMeetingQuery(guid);
        var queryHandler = new GetMeetingQuery.Handler(meetings);
        var meeting = await queryHandler.Handle(query, CancellationToken.None);
        logger.Info($"meeting: {meeting}");


    }
    //Logger : ILogger

}


