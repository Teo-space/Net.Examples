using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examples.GenericRepository;


public class Entity
{
    public Guid EntityId { get; set; }
    public string Name { get; set; }
}



public class EntityConfiguration : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        //builder.ToTable("Entities");
        builder.Property(x => x.EntityId).ValueGeneratedOnAdd();
        builder.HasKey(x => x.EntityId);
        builder.Property(x => x.Name).HasMaxLength(255);

    }

}

