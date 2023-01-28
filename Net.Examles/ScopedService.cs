using Net.Examles.ExampesOrm.TestScenarios;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesFluentBranches;
using Net.Examles.ExamplesObservable;
using Net.Examles.ExamplesObservableCollection;

public record ScopedService(
    IServiceScopeFactory serviceScopeFactory, ILogger<ScopedService> logger) 
    
    
    : HostedService(serviceScopeFactory, logger)
{
    public override async Task Scope(IServiceProvider serviceProvider, CancellationToken token)
    {
        //await Handle<CryptographyExample>(token);
        //await Handle<ObservableCollectionExample>(token);
        //await Handle<ObservableExample>(token);
        //await Handle<FluentBrancheExample>(token);


        //await Handle<ScenarioLinq2Db>(token);
        //services.AddScoped<ScenarioEFCore>();

        await Handle<ScenarioEFCore>(token);










    }
}