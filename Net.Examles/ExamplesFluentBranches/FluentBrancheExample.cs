namespace Net.Examles.ExamplesFluentBranches;


public record FluentBrancheExample(ILogger<FluentBrancheExample> logger) : Handler
{
    void print(string message) => logger.Info(message);
    public async Task Handle(CancellationToken token)
    {
        foreach(var e in Enumerable.Range(0, 100).If(x => x > 5 && x < 10).If(x => x > 20 && x < 25))
        {
            print($"{e}");
        }


    }

}
