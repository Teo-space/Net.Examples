using System.Buffers;
using System.Threading.Tasks;

namespace Net.Examles.Tools.BenchMark;


internal class BenchChain
{
    record BenchResult(string Name, double milliSeconds, string ExceptionMessage);




    List<BenchResult> BenchResults = new();


    Dictionary<string, Task> BenchTasks = new();

    public BenchChain Add(string name, Task task)
    {
        BenchTasks.Add(name, task);
        return this;
    }

    public async Task<BenchChain> RunAsync()
    {
        DateTime Start = DateTime.Now;
        DateTime dateTime = DateTime.Now;

        foreach (var item in BenchTasks)
        {
            dateTime = DateTime.Now;
            string ExceptionMessage = string.Empty;
            try
            {
                await item.Value;
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }

            var result = new BenchResult(
                item.Key, 
                DateTime.Now.Subtract(dateTime).TotalMilliseconds, 
                ExceptionMessage);

            BenchResults.Add(result);
        }
        BenchResults.Add(new BenchResult("Total", DateTime.Now.Subtract(Start).TotalMilliseconds, string.Empty));

        return this;
    }



    Dictionary<string, Action> BenchElements = new();

    public BenchChain Add(string name, Action action)
    {
        BenchElements.Add(name, action);
        return this;
    }

    public BenchChain Run()
    {
        DateTime Start = DateTime.Now;
        DateTime dateTime = DateTime.Now;

        foreach (var item in BenchElements)
        {
            dateTime = DateTime.Now;
            string ExceptionMessage = string.Empty;
            try
            {
                item.Value();
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }

            var result = new BenchResult(
                item.Key,
                DateTime.Now.Subtract(dateTime).TotalMilliseconds,
                ExceptionMessage);

            BenchResults.Add(result);
        }
        BenchResults.Add(new BenchResult("Total", DateTime.Now.Subtract(Start).TotalMilliseconds, string.Empty));

        return this;
    }




    public override string ToString() => BenchResults.ToConsoleTable().ToString();
}
