using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Models;


internal class DbObject
{
    public Guid DbObjectId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }


    public Guid ObjectTypeId { get; set; }
    public DbObjectType ObjectType { get; set; }

    public Guid AreaId { get; set; }
    public DbSubjectArea Area { get; set; }

    public List<DbObjectAttribute> Attributes { get; set;} = new();


    public DbRelation Parent { get; set; }
    public List<DbRelation> Childs { get; set;} = new();
}

internal class DbObjectConfiguration : IEntityTypeConfiguration<DbObject>
{
    //builder.Property(x => x.DbObjectId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbObject> b)
    {
        b.ToTable("DbObjects");
        b.HasIndex(x => x.DbObjectId);

        b.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        b.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();

        b.Property(x => x.Name).IsRequired().HasMaxLength(300);
        b.Property(x => x.Description).IsRequired().HasMaxLength(1000);

        //ObjectType
        b.HasOne(o => o.ObjectType).WithMany(oType => oType.DbObjects).HasForeignKey(x => x.ObjectTypeId)
            .OnDelete(DeleteBehavior.NoAction);
        //Area
        b.HasOne(o => o.Area).WithMany(a => a.DbObjects).HasForeignKey(x => x.AreaId)
            .OnDelete(DeleteBehavior.NoAction);
        //Attributes
        b.HasMany(o => o.Attributes).WithOne(attr => attr.DbObject).HasForeignKey(attr => attr.DbObjectId)
            .OnDelete(DeleteBehavior.NoAction);


        //Parent
        //b.HasOne(o => o.Parent).WithOne(rel => rel.ChildrenObject)
        //    //.HasForeignKey<DbRelation>(rel => rel.ChildrenObjectId).OnDelete(DeleteBehavior.Restrict)
        //    ;
        ////Childrens
        //b.HasMany(o => o.Childs).WithOne(rel => rel.ParentObject)
        //    //.HasForeignKey(rel => rel.ParentObjectId).OnDelete(DeleteBehavior.Restrict)
        //    ;



    }
}