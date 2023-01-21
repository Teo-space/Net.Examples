using Net.Examles;

public class Program
{
    public static void Main(string[] args)
    {
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(ServiceConfiguration.Configure)
            .ConfigureServices(RunServices.Run)
            .Build()
            .Run()
            ;
    }

}
