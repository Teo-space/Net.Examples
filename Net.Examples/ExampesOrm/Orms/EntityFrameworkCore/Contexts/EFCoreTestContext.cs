using ExampesOrm.Models;


namespace Net.OrmTests.Orms.EntityFrameworkCore.Contexts;

public class EFCoreTestContext : DbContext
{
	public DbSet<Category> Categories { get; set; }

	public DbSet<Product> Products { get; set; }

	public EFCoreTestContext()
	{
		Database.EnsureCreated();
	}

    public EFCoreTestContext(DbContextOptions<EFCoreTestContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
        //options.UseSqlite($"Data Source=EFCoreTestContext.db");
        //options.LogTo(Console.WriteLine);
        //options.UseNpgsql("User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL");
        options.UseNpgsql(
@"UserName=postgres;
Password=3p33c7u7s6;
Host=localhost;
Port=5432;
Database=postgres;");

    }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

}
