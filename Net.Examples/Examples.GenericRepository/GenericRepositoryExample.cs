using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Net.Examples.Examples.GenericRepository.Interfaces;
using System.Linq.Expressions;

namespace Net.Examples.Examples.GenericRepository;


public record GenericRepositoryExample(ILogger<GenericRepositoryExample> logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
        logger?.Info($"{nameof(GenericRepositoryExample)}");

        IServiceCollection services = new ServiceCollection();

        services.AddDbContext<GenericRepoDbContext>(options => options
            .UseInMemoryDatabase("Test"));
            //.UseSqlite($"Data Source=InMemorySample;Mode=Memory;Cache=Shared;"));

        services.AddScoped<EntityRepo>();


        logger?.Info($"BuildServiceProvider");
        var serviceProvider = services.BuildServiceProvider();

        logger?.Info($"GetRequiredService<EntityRepo");
        var repo = serviceProvider.GetRequiredService<EntityRepo>();







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


}



public record EntityRepo(GenericRepoDbContext context) : GenericRepositoryBase<Entity, Guid>(context);



public record GenericRepositoryBase<TEntity, TId>(GenericRepoDbContext context)
    : IGenericRepository<TEntity, TId>

    where TEntity : class
{
    public IEnumerable<TEntity> GetAll() => context.Set<TEntity>().ToList().AsReadOnly();


    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicateWhere) =>
        context.Set<TEntity>().Where(predicateWhere).ToList().AsReadOnly();

    public TEntity? GetById(TId Id)
        => context.Find<TEntity>(Id);

    public int Count()
        => context.Set<TEntity>().Count();




    public bool Create(TEntity entity)
    {
        context.Add(entity);
        return context.SaveChanges() > 0;
    }


    public bool Update(TEntity entity)
    {
        context.Update(entity);
        return context.SaveChanges() > 0;
    }

    public bool Save(TEntity entity)
    {
        context.Attach(entity);
        return context.SaveChanges() > 0;
    }


    public bool RemoveByid(Guid Id)
    {
        var e = context.Find<TEntity>(Id);
        if (e != default)
        {
            //context.Set<Entity>().Remove(e);
            context.Remove(e);
            return context.SaveChanges() > 0;
        }
        return false;
    }

    public bool RemoveRange(params TEntity[] entities)
    {
        context.RemoveRange(entities);
        //context.Set<Entity>().RemoveRange
        return context.SaveChanges() > 0;
    }


    public bool RemoveWhere(Expression<Func<TEntity, bool>> predicateWhere)
    {
        var entitiesToRemove = context.Set<TEntity>().Where(predicateWhere).ToArray();
        if (entitiesToRemove.Length > 0)
        {
            context.RemoveRange(entitiesToRemove);
            return context.SaveChanges() > 0;
        }
        return true;
    }




}
