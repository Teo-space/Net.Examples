using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Предметная область
/// </summary>
internal class DbSubjectArea
{
    public Guid DbSubjectAreaId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<DbObject> DbObjects { get; set; } = new();
}
/// <summary>
/// Конфиг для EFCore
/// </summary>
internal class DbSubjectArea__Configuration : IEntityTypeConfiguration<DbSubjectArea>
{
    //builder.Property(x => x.DbObjectId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbSubjectArea> b)
    {
        b.ToTable("DbSubjectAreas");

        b.HasIndex(x => x.DbSubjectAreaId);

        b.Property(x => x.Name).IsRequired().HasMaxLength(300);
        b.Property(x => x.Description).IsRequired().HasMaxLength(1000);


        b.HasMany(a => a.DbObjects)
            .WithOne(o => o.Area)
            .HasForeignKey(o => o.AreaId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}