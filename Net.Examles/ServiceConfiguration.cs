using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using Microsoft.Extensions.Options;
using Net.Examles.ExampesOrm.TestScenarios;
using Net.Examles.ExamplesCryptography;
using Net.Examles.ExamplesFluentBranches;
using Net.Examles.ExamplesObservable;
using Net.Examles.ExamplesObservableCollection;
using Net.Examles.Tools.Logger;
using Net.OrmTests.Orms.EntityFrameworkCore.Contexts;
using Net.OrmTests.Orms.Linq2Db.Contexts;
using Serilog;

public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AutoConfigure();


        services.AddDbContext<EFCoreTestContext>(options =>
            //x.UseSqlite($"Data Source=EFCoreTestContext.db")
            options.UseNpgsql("User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL")
        ); ;

        
        services.AddLinqToDBContext<Linq2DBContext>((provider, options) =>
            //options.UseConnectionString(LinqToDB.ProviderName.SQLite, $"Data Source={SQLiteFilePath};Version=3;");
            //options.UseSQLite($"Data Source=Linq2DBContext.db;Version=3;")
            //.UseDefaultLogging(provider)
            //;
            options.UsePostgreSQL("User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL")
        );







        
    }



}
public static class RunServices
{
    public static void Run(IServiceCollection services)
    {
        services.AddScoped<CryptographyExample>();


        services.AddScoped<ObservableCollectionExample>();
        services.AddScoped<ObservableExample>();

        services.AddScoped<FluentBrancheExample>();


        services.AddScoped<ScenarioLinq2Db>();
        services.AddScoped<ScenarioEFCore>();



    }


}