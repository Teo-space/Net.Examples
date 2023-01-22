using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using Microsoft.Extensions.Options;
using Net.Examles.Tools.Logger;
using Net.OrmTests.Orms.EntityFrameworkCore.Contexts;
using Net.OrmTests.Orms.Linq2Db.Contexts;
using Serilog;

public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        //services.AddLogging();
        //services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        //LoggerGeneric<T> : ILogger<T>

        //services.AddScoped<ILogger, Logger>();
        //services.AddScoped(typeof(ILogger<>), typeof(LoggerGeneric<>));



        services.AddDbContext<EFCoreTestContext>(x => x.UseSqlite($"Data Source=EFCoreTestContext.db"));

        services.AddLinqToDBContext<Linq2DBContext>((provider, options) =>
        {
            //options.UseConnectionString(LinqToDB.ProviderName.SQLite, $"Data Source={SQLiteFilePath};Version=3;");

            options.UseSQLite($"Data Source=Linq2DBContext.db;Version=3;")
            .UseDefaultLogging(provider)
            ;
        });













        AutoConfigure(services);
    }



    public static void AutoConfigure(IServiceCollection services)
    {
        Console.WriteLine($"AutoConfigure", ConsoleColor.Red);
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.GetInterfaces().Any(i => i == typeof(IConfiguration)))
            {
                try
                {
                    //Console.WriteLine($"constructor: {type.Name}", ConsoleColor.Red);

                    var constructor = type.GetConstructors()
                        .Where(ctor => ctor.GetParameters().Length == 0)
                        .FirstOrDefault();

                    //Console.WriteLine($"Configuration", ConsoleColor.Red);
                    var o = constructor.Invoke(new object[] { });
                    var Configuration = (IConfiguration)o;

                    //Console.WriteLine($"Configuration.Configure", ConsoleColor.Red);
                    Configuration.Configure(services);

                    //Call Configuration.Configure(services);
                    //type?.GetMethod(nameof(IConfiguration.Configure))?.Invoke(Configuration, new object[] { services });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString(), ConsoleColor.Red);
                }
            }
        }
    }

}