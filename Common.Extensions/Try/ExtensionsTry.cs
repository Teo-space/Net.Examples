namespace Common.Extensions.Try;

public static class ExtensionsTry
{
    public static void Try<T>(this T o, Action<T> action)
    {
        try
        {
            action(o);
        }
        catch { }
    }

}
