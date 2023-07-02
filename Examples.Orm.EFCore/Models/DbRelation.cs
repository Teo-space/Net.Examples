using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Models;

/// <summary>
/// Связи между объектами
/// </summary>
internal class DbRelation
{
    public Guid DbRelationId { get; set; }


    public Guid DbRelationTypeId { get; set; }
    public DbRelationType RelationType { get; set; }

    public Guid ParentObjectId { get; set; }
    public DbObject ParentObject { get; set; }

    public Guid ChildrenObjectId { get; set; }
    public DbObject ChildrenObject { get; set; }
}

internal class DbRelation__Configuration : IEntityTypeConfiguration<DbRelation>
{
    //builder.Property(x => x.DbObjectId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbRelation> b)
    {
        b.ToTable("DbRelations");
        b.HasIndex(x => x.DbRelationId);

        //RelationType
        b.HasOne(rel => rel.RelationType).WithMany(type => type.DbRelations).HasForeignKey(rel => rel.DbRelationId)
            .OnDelete(DeleteBehavior.NoAction);
        //ParentObject
        b.HasOne(rel => rel.ParentObject).WithMany(o => o.Childs)
            .HasForeignKey(rel => rel.ParentObjectId).OnDelete(DeleteBehavior.Restrict);
        //ChildrenObject
        b.HasOne(rel => rel.ChildrenObject).WithOne(o => o.Parent)
            .HasForeignKey<DbRelation>(rel => rel.ChildrenObjectId).OnDelete(DeleteBehavior.Restrict);

    }
}

