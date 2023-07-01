using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Models;

internal class DbRelationType
{
    public Guid DbRelationTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<DbRelation> DbRelations { get; set; } = new();
}
internal class DbRelationType__Configuration : IEntityTypeConfiguration<DbRelationType>
{
    //builder.Property(x => x.DbObjectId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbRelationType> b)
    {
        b.ToTable("DbRelationTypes");

        b.HasIndex(x => x.DbRelationTypeId);

        b.Property(x => x.Name).IsRequired().HasMaxLength(300);
        b.Property(x => x.Description).IsRequired().HasMaxLength(1000);


        b.HasMany(t => t.DbRelations).WithOne(r => r.RelationType).HasForeignKey(r => r.DbRelationTypeId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}