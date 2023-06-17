namespace Common.Logging.Logger;

public class Logger : LoggerBase, ILogger
{
    public void Info(string message) => print(message, ConsoleColor.White);
    public void Info(params object[] parameters) => print(parameters);


    public void Warn(string message) => print(message, ConsoleColor.Yellow);

    public void Error(string message) => print(message, ConsoleColor.DarkRed);


    public void Error(string message, Exception exception)
    {
        print(message, ConsoleColor.DarkRed);
        print(exception.Message, ConsoleColor.DarkRed);
        print(exception.ToString(), ConsoleColor.DarkRed);
    }

    public void Error(Exception exception)
    {
        print(exception.Message, ConsoleColor.DarkRed);
        print(exception.ToString(), ConsoleColor.DarkRed);
    }


}

