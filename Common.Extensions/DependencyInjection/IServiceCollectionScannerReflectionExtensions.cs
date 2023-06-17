using System.Collections.ObjectModel;



public static class ReflectionExtensions
{
    public static IEnumerable<Type> GetBaseTypes(this Type type)
    {
        if (type.IsGenericType)
        {
            //yield return type.GetGenericTypeDefinition();
        }
        while (type != null && type != typeof(object))
        {
            foreach (Type tInterface in type.GetInterfaces())
            {
                if (tInterface.IsGenericType)
                {
                    yield return tInterface.GetGenericTypeDefinition();
                }
                else yield return tInterface;
            }
            type = type.BaseType;
            if (type != null && type != typeof(object))
            {
                if (type.IsGenericType)
                {
                    yield return type.GetGenericTypeDefinition();
                }
                else yield return type;
            }
        }
    }


    public static IEnumerable<Type> GetBaseInterfaces(this Type type)
    {
        while (type != null && type != typeof(object))
        {
            foreach (Type tInterface in type.GetInterfaces())
            {
                if (tInterface.IsGenericType)
                {
                    yield return tInterface.GetGenericTypeDefinition();
                }
                else yield return tInterface;
            }
            type = type.BaseType;
        }
    }


    public static bool IsBasedOn(this Type type, Type otherType)
    {
        if (otherType.IsGenericTypeDefinition)
        {
            return type.IsAssignableToGenericTypeDefinition(otherType);
        }
        return otherType.IsAssignableFrom(type);
    }

    private static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
    {
        foreach (Type t in type.GetInterfaces())
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }
        }
        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        {
            return true;
        }
        return type.BaseType != null && type.BaseType.IsAssignableToGenericTypeDefinition(genericType);
    }



    public static IServiceCollection AddAssignbledTo
        (this IServiceCollection services, Assembly assembly, params Type[] AssignableTypes)
    {
        var types = assembly.GetTypes()
            .Where(x => !x.IsInterface)
            .Where(t => AssignableTypes
            .Any(assignable => t.IsBasedOn(assignable)))
            .ToArray();

        foreach (Type t in types)
        {
            //Console.WriteLine($"Type: {t.FullName}");
            services.AddScoped(t);
        }

        var Interfaces = types
            .SelectMany(t => t.GetInterfaces().Select(tInterface => new KeyValuePair<Type, Type>(tInterface, t)))
            .ToDictionary()
            ;

        foreach (var Interface in Interfaces)
        {
            //Console.WriteLine($"Interface: {Interface.Key} - {Interface.Value}");
            services.AddScoped(Interface.Key, Interface.Value);
        }


        return services;
    }








    public class ServicesWithTypes
    {
        public IServiceCollection services { get; private set; }
        public IReadOnlyList<Type> Types { get; private set; }
        public ServicesWithTypes(IServiceCollection services, IReadOnlyList<Type> types)
        {
            this.services = services;
            Types = types;
        }
    }

    public static IReadOnlyList<Type> Types(params Assembly[] assemblies)
        => new ReadOnlyCollection<Type>(assemblies.SelectMany(a => a.GetTypes()).ToArray());

    public static ServicesWithTypes FromAssemblies
        (this IServiceCollection services, params Assembly[] assemblies)
        => new ServicesWithTypes(services, Types(assemblies));
    public static ServicesWithTypes FromAssembly
        (this IServiceCollection services, Assembly assembly)
        => new ServicesWithTypes(services, Types(assembly));

    public static ServicesWithTypes FromExecutingAssembly
        (this IServiceCollection services, params Assembly[] assemblies)
        => new ServicesWithTypes(services, Types(Assembly.GetExecutingAssembly()));
    public static ServicesWithTypes FromCallingAssembly
        (this IServiceCollection services, params Assembly[] assemblies)
        => new ServicesWithTypes(services, Types(Assembly.GetCallingAssembly()));



    public static ServicesWithTypes AssignableTo(this ServicesWithTypes services, params Type[] AssignableTypes)
    {
        var types = new ReadOnlyCollection<Type>(services.Types
            .Where(x => !x.IsInterface)
            .Where(t => AssignableTypes
            .Any(assignable => t.IsBasedOn(assignable)))
            .ToArray());
        return new ServicesWithTypes(services.services, types);
    }

    public static ServicesWithTypes AssignableTo<T>
        (this ServicesWithTypes services) => services.AssignableTo(typeof(T));


    public class ServicesWithAssignabledAs
    {
        public IServiceCollection services { get; private set; }
        public IReadOnlyDictionary<Type, Type> Types { get; private set; }

        public ServicesWithAssignabledAs(IServiceCollection services, ReadOnlyDictionary<Type, Type> types)
        {
            this.services = services;
            Types = types;
        }
        public ServicesWithAssignabledAs(ServicesWithTypes services, ReadOnlyDictionary<Type, Type> types)
        {
            this.services = services.services;
            Types = types;
        }
    }

    public static ServicesWithAssignabledAs AsSelf(this ServicesWithTypes services)
        => new ServicesWithAssignabledAs(services, new ReadOnlyDictionary<Type, Type>(services.Types.ToDictionary(t => t)));

    public static ServicesWithAssignabledAs AsImplementedInterfaces(this ServicesWithTypes services)
    {
        var types = services.Types
            .SelectMany(t => t
            .GetInterfaces()
            .Select(tInterface => new KeyValuePair<Type, Type>(tInterface, t)))
            .ToDictionary();
        return new ServicesWithAssignabledAs(services, new ReadOnlyDictionary<Type, Type>(types));
    }
    public static ServicesWithAssignabledAs AsSelfWithInterfaces(this ServicesWithTypes services)
    {
        var self = services.Types.ToDictionary(t => t);
        var types = services.Types
            .SelectMany(t => t
            .GetInterfaces()
            .Select(tInterface => new KeyValuePair<Type, Type>(tInterface, t)))
            .Union(self)
            .ToDictionary()
            ;
        return new ServicesWithAssignabledAs(services, new ReadOnlyDictionary<Type, Type>(types));
    }


}
