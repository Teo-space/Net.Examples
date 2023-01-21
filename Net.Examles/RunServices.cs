using Net.Examles;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesObservableCollection;


public static class RunServices
{
    public static void Run(IServiceCollection services)
    {
        //services.AddHostedService<Worker>();

        //services.AddHostedService<CryptographyExample>();
        services.AddHostedService<ObservableCollectionExample>();










    }


}