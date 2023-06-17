/// <summary>
/// Помогает выгрузить все зависимости в контейнер
/// </summary>
public static class ExtensionsDependencyLoader
{
    /// <summary>
    /// Помогает выгрузить все зависимости в контейнер
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    public static void AddDependencies<T>(this IServiceCollection services) 
        => ExtensionsDependencyLoader.AddDependencies(services, typeof(T));



    /// <summary>
    /// Помогает выгрузить все зависимости в контейнер
    /// </summary>
    /// <param name="services"></param>
    /// <param name="DependencyType"></param>
    public static void AddDependencies(this IServiceCollection services, Type DependencyType) 
    {
        ExtensionsDependencyLoader.AddDependencies(services, Assembly.GetExecutingAssembly(), DependencyType);

        //добавить
        //Assembly.GetCallingAssembly
        //Assembly.GetEntryAssembly
    }


    public static void AddDependencies(this IServiceCollection services, Assembly assembly, Type DependencyType)
    {
        foreach (var type in assembly.GetTypes()
            .Where(type => type.GetInterfaces().Any(Interface => Interface == DependencyType)))
        {
            Console.WriteLine($"[DependencyLoader] Add : {type.Name}", ConsoleColor.DarkMagenta);
            services.AddScoped(type);
        }
    }

}

