static class ExtensionsAutoConfigure
{
    public static void AutoConfigure(this IServiceCollection services)
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