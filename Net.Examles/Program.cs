public class Program
{
    public static void Main(string[] args)
    {

        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices(services =>
            {
                services.AddHostedService<ScopedService>();
            })
            .ConfigureServices(ServiceConfiguration.Configure)
            .ConfigureServices(RunServices.Run)
            .Build()
        .Run()
        ;
    }
}











