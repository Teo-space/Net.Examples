using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Net.Examles;
using static Org.BouncyCastle.Math.EC.ECCurve;

public class Program
{
    public static void Main(string[] args)
    {

        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices(ServiceConfiguration.Configure)
            .ConfigureServices(RunServices.Run)
            .Build()
            .Run()
            ;

        //config.Options = ConfigOptions.DisableOptimizationsValidator;
        //BenchmarkRunner.Run<StringTest>(DefaultConfig.Instance.With(ConfigOptions.DisableOptimizationsValidator));
    }

}



public class StringTest
{
    string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };


    [Benchmark]
    public string WithStringBuilder()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string s in numbers)
        {
            stringBuilder.Append(s);
            stringBuilder.Append(" ");
        }
        return stringBuilder.ToString();
    }


    [Benchmark]
    public string WithConcatenation()
    {
        string result = "";
        foreach (string s in numbers) result = result + s + " ";
        return result;
    }


    [Benchmark]
    public string WithInterpolation()
    {
        string result = "";
        foreach (string s in numbers) result = $"{result}{s} ";
        return result;
    }
}






