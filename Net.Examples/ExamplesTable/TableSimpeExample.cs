namespace Net.Examles.ExamplesTable;


public record TableSimpeExample(ILogger logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
        DateTime date = DateTime.Now;
        int c = 0;


        int YLength = 10000000;
        int XLength = 5;

        object[,] table = new object[YLength + 1, 6];

        table[0, 0] = "aaaaaaaaa";
        table[0, 1] = "bbbbbbbbb";
        table[0, 2] = "ccccccccc";
        table[0, 3] = "ddddddddd";
        table[0, 4] = "eeeeeeeee";
        table[0, 5] = "fffffffff";



        for (int y = 1; y < YLength + 1; y++)
        {
            for (int x = 0; x < XLength; x++)
            {
                table[y, x] = c++;
            }
        }



        Console.WriteLine($"WorkingSet64        {System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024)}");
        Console.WriteLine($"TotalMilliseconds: {(DateTime.Now - date).TotalMilliseconds}");
        date = DateTime.Now;


        //var slice = table.Slice(0, 3);
        var slice = table.Slice("aaaaaaaaa", "ccccccccc", "fffffffff");


        Console.WriteLine($"WorkingSet64        {System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024)}");
        Console.WriteLine($"TotalMilliseconds: {(DateTime.Now - date).TotalMilliseconds}");
        date = DateTime.Now;

        //Console.WriteLine($"{table.Head()}");
        //Console.WriteLine($"{slice.Head()}");

        var dt = table.ToTable();
        Console.WriteLine($"ToTable dt");
        Console.WriteLine($"WorkingSet64        {System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024)}");
        Console.WriteLine($"TotalMilliseconds: {(DateTime.Now - date).TotalMilliseconds}");
        date = DateTime.Now;


        var dtSlice = slice.ToTable();
        Console.WriteLine($"ToTable dtSlice");
        Console.WriteLine($"WorkingSet64        {System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024)}");
        Console.WriteLine($"TotalMilliseconds: {(DateTime.Now - date).TotalMilliseconds}");
        date = DateTime.Now;


        Console.ReadLine();
    }
}


