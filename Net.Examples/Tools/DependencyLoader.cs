using Net.Examles.Interfaces;

namespace Net.Examles.Tools;


public static class DependencyLoader
{
    public static void AddDependencies(this IServiceCollection services)
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(Interface => Interface == typeof(IDependency))))
        {
            services.AddScoped(type);
        }
    }


    public static void AddDependencies<T>(this IServiceCollection services)
    {
        var DependencyType = typeof(T);

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(Interface => Interface == DependencyType)))
        {
            services.AddScoped(type);
        }
    }


    public static void AddDependencies<T>(this IServiceCollection services, Type DependencyType)
    {
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.GetInterfaces().Any(Interface => Interface == DependencyType)))
        {
            services.AddScoped(type);
        }
    }


}
