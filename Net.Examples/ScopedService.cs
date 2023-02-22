using Microsoft.AspNetCore.DataProtection;
using Net.Examles.ExampesOrm.TestScenarios;
using Net.Examles.Examples.CQRS;
using Net.Examles.Examples.CQRS.Manual;
using Net.Examles.Examples.CQRS.MediarR;
using Net.Examles.Examples.CQRS.Scrutor;
using Net.Examles.Examples.Mapping.AutoMapper;
using Net.Examles.Examples.Mapping.Mapster;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesFluentBranches;
using Net.Examles.ExamplesObservable;
using Net.Examles.ExamplesObservableCollection;
using Net.Examples.Examples.GenericRepository;

public record ScopedService(
    IServiceScopeFactory serviceScopeFactory, 
    ILogger<ScopedService> logger


    //IDataProtectionProvider provider

    ) 
    : HostedService(serviceScopeFactory, logger)
{

    //IDataProtector _protector;

    public override async Task Scope(IServiceProvider serviceProvider, CancellationToken token)
    {
        //await Handle<CryptographyExample>(token);
        //await Handle<ObservableCollectionExample>(token);
        //await Handle<ObservableExample>(token);
        //await Handle<FluentBrancheExample>(token);


        //await Handle<ScenarioLinq2Db>(token);
        //services.AddScoped<ScenarioEFCore>();

        //await Handle<ScenarioEFCore>(token);



        //await Handle<MeetingManualExample>(token);
        // await Handle<MeetingScrutorExample>(token);
        //await Handle<MeetingMediatRExample>(token);

        //await Handle<CQRSExample>(token);


        //await Handle<GenericRepositoryExample>(token);


        await Handle<AutoMapperExample>(token);
        await Handle<MapsterExample>(token);







        /*
        _protector = provider.CreateProtector("Contoso.MyClass.v1");


        Console.Write("Enter input: ");
        string input = Console.ReadLine();

        // protect the payload
        string protectedPayload = _protector.Protect(input);
        Console.WriteLine($"Protect returned: {protectedPayload}");

        // unprotect the payload
        //string unprotectedPayload = _protector.Unprotect(protectedPayload);
        //Console.WriteLine($"Unprotect returned: {unprotectedPayload}");

        */






    }
}