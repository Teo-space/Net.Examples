using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Infrastructure.Configuration;

internal static class ExtensionsAddDb
{

    public static IHostBuilder AddDbPostgres(this IHostBuilder builder, string connectionStringName = "postgres")
    {
        builder.ConfigureServices((hostContext, services) =>
        {
            string connectionString = hostContext.Configuration
            .GetConnectionString(connectionStringName)
            ?? throw new ArgumentNullException(nameof(connectionString));

            Console.WriteLine(connectionString);

            services.AddDbPostgres(connectionString);
        });
        return builder;
    }


    public static IHostBuilder AddDbMySql(this IHostBuilder builder, string connectionStringName = "mysql")
    {
        builder.ConfigureServices((hostContext, services) =>
        {
            string connectionString = hostContext.Configuration
            .GetConnectionString(connectionStringName) 
            ?? throw new ArgumentNullException(nameof(connectionString));

            services.AddDbMySql(connectionString);
        });

        return builder;
    }







    /// <summary>
    /// Добавляем и настраиваем контекст с Npgsql
    /// </summary>
    /// docker run --name postgres -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=SECRET -d postgres
    /// docker   run -d   --name some-postgres   -p 5432:5432 -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=SECRET   -e PGDATA=C:\VOLUMES\PSQL\pgdata    -v C:\VOLUMES\PSQL\data    postgres
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbPostgres(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseNpgsql(connectionString);
            //options.LogTo(Console.WriteLine);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    public static IServiceCollection AddDbMySql(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                //options.LogTo(Console.WriteLine);
                //.LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors()
                ;
        });

        return services;
    }


    /// <summary>
    /// 
    /// </summary>
    /// //docker pull mcr.microsoft.com/mssql/server:2022-latest
    /// //docker run -e "ACCEPT_EULA=Y" --name sql1 --hostname sql1 -p 1433:1433 -e "MSSQL_SA_PASSWORD=SECRET" -d mcr.microsoft.com/mssql/server:2022-latest
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection DbAddSqlServer(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseSqlServer(connectionString)
            //options.LogTo(Console.WriteLine)
            ;
        });

        return services;
    }



    public static IServiceCollection DbAddInMemory(this IServiceCollection services, string databaseName = "Application")
    {
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName)
            //.LogTo(Console.WriteLine)
            ;
        });

        return services;
    }


    public static IServiceCollection DbAddSqlite(this IServiceCollection services)
    {
        services.AddDbContext<AppDataContext>(options =>
        {
            options.UseSqlite($"Data Source=AppDataContext.db")
            //options.LogTo(Console.WriteLine)
            ;
        });

        return services;
    }



}
