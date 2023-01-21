using Net.Examles;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesObservable;
using Net.Examles.ExamplesObservableCollection;


public static class RunServices
{
    public static void Run(IServiceCollection services)
    {
        //services.AddHostedService<CryptographyExample>();

        //services.AddHostedService<ObservableCollectionExample>();
        services.AddHostedService<ObservableExample>();










    }


}