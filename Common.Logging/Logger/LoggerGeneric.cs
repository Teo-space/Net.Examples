namespace Common.Logging.Logger;

public class LoggerGeneric<T> : LoggerBase, ILogger<T>
{
    string TypeName
    {
        get => typeof(T).Name;
    }


    public void Info(string message) => print($"[{TypeName}] {message}", ConsoleColor.White);
    public void Info(params object[] parameters)
    {
        print($"[{TypeName}]", ConsoleColor.White);
        print(parameters);
    }


    public void Warn(string message) => print($"[{TypeName}] {message}", ConsoleColor.Yellow);

    public void Error(string message) => print($"[{TypeName}] {message}", ConsoleColor.DarkRed);


    public void Error(string message, Exception exception)
    {
        print($"[{TypeName}] {message}", ConsoleColor.DarkRed);
        print(exception.Message, ConsoleColor.DarkRed);
        print(exception.ToString(), ConsoleColor.DarkRed);
    }

    public void Error(Exception exception)
    {
        print($"[{TypeName}] {exception.Message}", ConsoleColor.DarkRed);
        print(exception.ToString(), ConsoleColor.DarkRed);
    }


}