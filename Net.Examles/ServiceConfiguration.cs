public static class ServiceConfiguration
{
    public static void Configure(IServiceCollection services)
    {
        services.AddLogging();







        AutoConfigure(services);
    }

    public static void AutoConfigure(IServiceCollection services)
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.GetInterfaces().Any(i => i == typeof(IConfiguration)))
            {
                try
                {
                    Console.WriteLine($"type: {type.Name}", ConsoleColor.Red);

                    var constructor = type.GetConstructors()
                        .Where(ctor => ctor.GetParameters().Length == 0)
                        .FirstOrDefault();

                    var Configuration = constructor.Invoke(new object[] { });

                    //Call Configuration.Configure(services);
                    type?.GetMethod(nameof(IConfiguration.Configure))?.Invoke(Configuration, new object[] { services });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString(), ConsoleColor.Red);
                }
            }
        }
    }

}