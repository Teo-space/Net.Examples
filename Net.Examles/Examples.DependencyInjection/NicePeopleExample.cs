namespace Net.Examles.Examples.DependencyInjection;



public record NicePeopleExample(
            ILogger<NicePeopleExample> logger,
            IGenericRepository<NicePeople, Guid> nicePeopleRepo) 
    : Handler
{
    public async Task Handle(CancellationToken token)
    {
        logger.Info("NicePeopleWorker");

        var Id = nicePeopleRepo.Add(new NicePeople("Ron", 27));

        var people = nicePeopleRepo.Get(Id);

        logger.Info(LogLevel.Information, people.ToString());


    }


}