using Examples.Orm.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Infrastructure;

internal class AppDataContext : DbContext
{
    //Database.Try(x => x.EnsureDeleted());
    public AppDataContext(DbContextOptions<AppDataContext> options, ILogger<AppDataContext> logger) 
        : base(options)
    {

        logger.Warn("Create DB");
        Database.EnsureCreated();
        logger.Warn("Done");
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)

    protected override void OnModelCreating(ModelBuilder builder) => builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



    public DbSet<DbSubjectArea> Areas { get; set; }

    public DbSet<DbObject> DbObjects { get; set; }
    public DbSet<DbObjectType> DbObjectTypes { get; set; }

    public DbSet<DbObjectAttribute> DbObjectAttributes { get; set; }


    public DbSet<DbRelation> DbRelations { get; set; }
    public DbSet<DbRelationType> DbRelationTypes { get; set; }



}
