namespace Net.Examles.Tools.Logger;


internal class Logger : ILogger
{


    static DateTime start = DateTime.Now;

    static string Time() => $"{DateTime.Now}:{DateTime.Now.Millisecond}";

    static void print(object o = null) => print(o, ConsoleColor.White);

    static void print(object o = null, ConsoleColor color = ConsoleColor.White)
    {
        if (o == null)
        {
            Console.WriteLine("null");
            return;
        }
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(Time());
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("]");

        Console.ForegroundColor = color;
        Console.Write("  ");
        Console.Write(o.ToString());

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("  ");
        Console.Write($"({Math.Round((DateTime.Now - start).TotalMilliseconds, 2)})        ");
        start = DateTime.Now;

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

    }

    static void print(params object[] parameters)
    {
        if (parameters == null || parameters.Length == 0) return;

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(Time());
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("]");

        Console.Write("  ");
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < parameters.Length; i++)
        {
            Console.Write($"  {parameters[i]}");
        }

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("  ");
        Console.Write($"({Math.Round((DateTime.Now - start).TotalMilliseconds, 2)})        ");
        start = DateTime.Now;

        Console.WriteLine();
    }






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