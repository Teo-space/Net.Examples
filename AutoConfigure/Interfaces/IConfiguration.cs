using Microsoft.Extensions.DependencyInjection;


public interface IConfiguration
{
    public void Configure(IServiceCollection services);
}


public class TestConfiguration : IConfiguration
{
    public void Configure(IServiceCollection services)
    {
        Console.WriteLine("TestConfiguration");
    }
}
