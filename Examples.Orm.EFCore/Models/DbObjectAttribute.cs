using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Models;

/// <summary>
/// Атрибуты объекта - кастомные свойства
/// </summary>
internal class DbObjectAttribute
{
    public Guid DbObjectAttributeId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public Guid DbObjectId { get; set; }
    public DbObject DbObject { get; set; }



    public string Name { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }

}
/// <summary>
/// Конфиг для EFCore
/// </summary>
internal class DbObjectAttribute__Configuration : IEntityTypeConfiguration<DbObjectAttribute>
{
    //builder.Property(c => c.CategoryId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbObjectAttribute> b)
    {
        b.ToTable("DbObjectAttributes");
        b.HasIndex(c => c.DbObjectId);
        

        b.Property(c => c.CreatedAt).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAdd();
        b.Property(c => c.UpdatedAt).IsRequired().HasDefaultValueSql("NOW()").ValueGeneratedOnAddOrUpdate();

        b.Property(c => c.Name).IsRequired().HasMaxLength(300);
        b.Property(c => c.Description).IsRequired().HasMaxLength(1000);
        b.Property(c => c.Value).IsRequired().HasMaxLength(1000);

        b.HasOne(a => a.DbObject)
            .WithMany(o => o.Attributes)
            .HasForeignKey(a => a.DbObjectId)
            .OnDelete(DeleteBehavior.Cascade);



    }
}