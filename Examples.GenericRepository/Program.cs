using Examples.GenericRepository;
using Microsoft.EntityFrameworkCore;


IServiceCollection services = new ServiceCollection();

services.AddLogging();

services.AddDbContext<GenericRepoDbContext>(options => options.UseInMemoryDatabase("Test"));
services.AddScoped<EntityRepository>();



var serviceProvider = services.BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger>();
var repo = serviceProvider.GetRequiredService<EntityRepository>();


RunTests(repo);



void RunTests(EntityRepository repo)
{

    int n = 300;

    logger?.Info($"Create");
    for (int i = 0; i < n; i++)
    {
        var entity = new Entity()
        {
            Name = $"test {i}",
        };
        var r = repo.Create(entity);
        //logger?.Info($"Create {r}");
    }
    logger?.Info($"Create.End");


    logger?.Info($"Save");
    for (int i = 0; i < n; i++)
    {
        var entity = new Entity()
        {
            Name = $"test {i}",
        };
        var r = repo.Save(entity);
        //logger?.Info($"Save {r}");
    }
    logger?.Info($"Save.End");


    logger?.Info($"Update");
    for (int i = 0; i < n; i++)
    {
        var entity = new Entity()
        {
            Name = $"test {i}",
        };
        var r = repo.Update(entity);
        //logger?.Info($"Update {r}");
    }
    logger?.Info($"Update.End");


    logger?.Info($"repo.Count {repo.Count()}");



    logger?.Info($"All");
    var all = repo.GetAll();
    logger?.Info($"All.Done");

    logger?.Info($"Edit");
    foreach (var entity in all)
    {
        entity.Name = entity.Name + "123";
        repo.Save(entity);
    }
    logger?.Info($"Edit.Saved");



    logger?.Info($"Remove");
    //foreach (var entity in all) repo.RemoveByid(entity.EntityId);
    //repo.RemoveRange(all.ToArray());
    repo.RemoveWhere(x => true);
    logger?.Info($"Remove.Done");
}

