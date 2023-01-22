using ExampesOrm.Models;


namespace Net.OrmTests.Orms.EntityFrameworkCore.Contexts;

public class EFCoreTestContext : DbContext
{
	public DbSet<Category> Categories { get; set; }

	public DbSet<Product> Products { get; set; }

	public EFCoreTestContext()
	{
		//print("EnsureCreated before", ConsoleColor.Yellow);
		Database.EnsureCreated();
		//print("EnsureCreated after", ConsoleColor.Green);
	}
	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseSqlite($"Data Source=EntityFrameworkCore{this.GetType().Name}.db");
		options.LogTo(Console.WriteLine);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

}
