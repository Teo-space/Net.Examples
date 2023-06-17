using Common.Extensions.DependencyInjection.Interfaces;


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
                    var constructor = type.GetConstructors()
                        .Where(ctor => ctor.GetParameters().Length == 0)
                        .FirstOrDefault();

                    //Console.WriteLine($"Configuration", ConsoleColor.Red);
                    var o = constructor.Invoke(new object[] { });
                    var Configuration = (IConfiguration)o;

                    //Console.WriteLine($"Configuration.Configure", ConsoleColor.Red);
                    Console.WriteLine($"[AutoConfigure] Add : {type.Name}", ConsoleColor.DarkMagenta);
                    Configuration.Configure(services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString(), ConsoleColor.Red);
                }
            }
        }
    }


}

