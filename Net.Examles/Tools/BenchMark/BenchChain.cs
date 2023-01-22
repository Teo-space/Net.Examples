using System.Buffers;
using System.Threading.Tasks;

namespace Net.Examles.Tools.BenchMark;


internal class BenchChain
{
    record BenchResult(string Name, double milliSeconds, string ExceptionMessage);

    List<BenchResult> BenchResults = new();
    Dictionary<string, Task> Tasks = new();

    public BenchChain Add(string name, Task task)
    {
        Tasks.Add(name, task);
        return this;
    }

    public async Task<BenchChain> Run()
    {
        DateTime dateTime = DateTime.Now;
        foreach (var item in Tasks)
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
        return this;
    }





}
