using Examples.Orm.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Infrastructure;

internal class AppDataContext : DbContext
{
    public AppDataContext() => Database.EnsureCreated();
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<DbObject>(entity =>
        {
            entity.HasMany(o => o.Childrens).WithOne(r => r.ParentObject).HasForeignKey(r => r.ParentObjectId).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(o => o.Parent).WithOne(r => r.ChildrenObject);
        });

        builder.Entity<DbRelation>(entity =>
        {
            entity.HasOne(r => r.ParentObject).WithMany(o => o.Childrens).HasForeignKey(r => r.ParentObjectId);//.OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.ChildrenObject).WithOne(r => r.Parent);
        });

    }


    public DbSet<DbSubjectArea> Areas { get; set; }

    public DbSet<DbObject> DbObjects { get; set; }
    public DbSet<DbObjectType> DbObjectTypes { get; set; }


    public DbSet<DbRelation> DbRelations { get; set; }
    public DbSet<DbRelationType> DbRelationTypes { get; set; }










}
