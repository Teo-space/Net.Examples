using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Examples.Orm.EFCore.Models;


/// <summary>
/// Тип объекта
/// </summary>
internal class DbObjectType
{

    public Guid DbObjectTypeId { get; set; }

    //Сделать уникальным в рамках одного объекта?
    //На уровне бд возможно или нет?
    //На уровне приложения
    public string Name { get; set; }
    public string Description { get; set; }

    public List<DbObject> DbObjects { get; set; } = new();
}


/// <summary>
/// Конфиг для EFCore
/// </summary>
internal class DbObjectType__Configuration : IEntityTypeConfiguration<DbObjectType>
{
    //builder.Property(c => c.CategoryId)//.ValueGeneratedOnAdd()
    public void Configure(EntityTypeBuilder<DbObjectType> b)
    {
        b.ToTable("DbObjectTypes");
        b.HasIndex(c => c.DbObjectTypeId);

        b.Property(c => c.Name).IsRequired().HasMaxLength(300);
        b.Property(c => c.Description).IsRequired().HasMaxLength(1000);

        b.HasMany(oType => oType.DbObjects)
            .WithOne(o => o.ObjectType)
            .HasForeignKey(o => o.ObjectTypeId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}