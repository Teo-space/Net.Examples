using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Examples.GenericRepository;


public class GenericRepoDbContext : DbContext
{
    public DbSet<Entity> Entities { get; set; }


    public GenericRepoDbContext() : base() => Database.EnsureCreated();
    public GenericRepoDbContext(DbContextOptions<GenericRepoDbContext> options) : base(options) => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}
