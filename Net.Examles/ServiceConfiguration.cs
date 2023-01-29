using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using Microsoft.Extensions.Options;
using Net.Examles.ExampesOrm.TestScenarios;
using Net.Examles.Examples.CQRS.Manual;
using Net.Examles.Examples.CQRS.Scrutor;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesFluentBranches;
using Net.Examles.ExamplesObservable;
using Net.Examles.ExamplesObservableCollection;
using Net.Examles.Tools.Logger;
using Net.OrmTests.Orms.EntityFrameworkCore.Contexts;
using Net.OrmTests.Orms.Linq2Db.Contexts;
using Net.Examles.Tools;
using Net.Examles.Interfaces;
using Net.Examles.Examples.CQRS.MediarR;

public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<ILogger, Logger>();

        services.AutoConfigure();


        services.AddDbContext<EFCoreTestContext>(options =>
            //x.UseSqlite($"Data Source=EFCoreTestContext.db")
            options.UseNpgsql("User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL")
        ); ;


        /*
        services.AddLinqToDBContext<Linq2DBContext>((provider, options) =>
            //options.UseConnectionString(LinqToDB.ProviderName.SQLite, $"Data Source={SQLiteFilePath};Version=3;");
            //options.UseSQLite($"Data Source=Linq2DBContext.db;Version=3;")
            //.UseDefaultLogging(provider)
            //;
            options.UsePostgreSQL("User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL")
        );
        */

        services.AddScoped<Linq2DBContext>();





    }



}
public static class RunServices
{
    public static void Run(IServiceCollection services)
    {
        //AddHandlers(services);
        //services.AddDependencies<IDependency>();
        //services.AddDependencies<Handler>();


        services.AddScoped<CryptographyExample>();


        services.AddScoped<ObservableCollectionExample>();
        services.AddScoped<ObservableExample>();

        services.AddScoped<FluentBrancheExample>();


        services.AddScoped<ScenarioLinq2Db>();
        services.AddScoped<ScenarioEFCore>();



        services.AddScoped<MeetingManualExample>();
        services.AddScoped<MeetingScrutorExample>();
        services.AddScoped<MeetingMediatRExample>();




    }

    /*
    //Handler
    //Handler
    public static void AddHandlers(IServiceCollection services)
    {
        Console.WriteLine("AddHandlers(IServiceCollection services)");

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Any(i => i == typeof(Handler))))
        {
            Console.WriteLine(type.FullName);

            services.AddScoped(type);
            services.AddScoped(typeof(Handler), type);
        }


    }
    */

}