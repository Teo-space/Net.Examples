using Net.Examles;
using Net.Examles.ExamplesCryptography;


public static class RunServices
{
    public static void Run(IServiceCollection services)
    {
        //services.AddHostedService<Worker>();

        services.AddHostedService<CryptographyExample>();










    }


}