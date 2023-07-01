using Common.Extensions.Try;
using Examples.Orm.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Examples.Orm.EFCore.Infrastructure;

internal class AppDataContext : DbContext
{
    //public AppDataContext(ILogger<AppDataContext> logger)
    //{
    //    logger.Warn("Delete DB");
    //    Database.EnsureDeleted();
    //    logger.Warn("Delete DB Done");
    //    logger.Warn("Create DB");
    //    Database.EnsureCreated();
    //    logger.Warn("Create DB Done");
    //}
    public AppDataContext(DbContextOptions<AppDataContext> options, ILogger<AppDataContext> logger)
        : base(options)
    {
        //logger.Warn("Delete DB");
        //Database.Try(x => x.EnsureDeleted());
        //// Database.EnsureDeleted();
        //logger.Warn("Done");

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        logger.Warn("Create DB");
        Database.EnsureCreated();
        logger.Warn("Done");
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;User ID=postgres;Password=SECRET;");
    //}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public DbSet<DbSubjectArea> Areas { get; set; }

    public DbSet<DbObject> DbObjects { get; set; }
    public DbSet<DbObjectType> DbObjectTypes { get; set; }

    public DbSet<DbObjectAttribute> DbObjectAttributes { get; set; }


    public DbSet<DbRelation> DbRelations { get; set; }
    public DbSet<DbRelationType> DbRelationTypes { get; set; }



}
