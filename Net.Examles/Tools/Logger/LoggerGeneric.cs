namespace Net.Examles.Tools.Logger;


internal class LoggerGeneric<T> : ILogger<T>
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
        //Console.Write($"({Math.Round((DateTime.Now - start).TotalMilliseconds, 2)})        ");
        //start = DateTime.Now;
        Console.ForegroundColor = color;
        Console.Write("  ");
        Console.Write(o.ToString());
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
        Console.WriteLine();
    }




    string TypeName
    {
        get => typeof(T).Name;
    }





    public void Info(string message) => print($"[{TypeName}] {message}", ConsoleColor.White);
    public void Info(params object[] parameters)
    {
        print($"[{TypeName}]", ConsoleColor.White);
        print(parameters, ConsoleColor.DarkGray);
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