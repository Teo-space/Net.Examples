namespace Net.Examles.ExamplesFluentBranches;


public class FluentBrancheExample : BackgroundService
{
    private readonly ILogger<FluentBrancheExample> logger;

    public FluentBrancheExample(ILogger<FluentBrancheExample> logger)
    {
        this.logger = logger;
    }


    void print(string message) => logger.Info(message);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        foreach(var e in Enumerable.Range(0, 100).If(x => x > 5 && x < 10).If(x => x > 20 && x < 25))
        {
            print($"{e}");
        }


    }

}
