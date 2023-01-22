using ExampesOrm.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampesOrm.Orms.EntityFrameworkCore.Configurations
{
	public class ICategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Categories");
			builder.HasIndex(c => c.CategoryId);
			builder.Property(c => c.CategoryId).ValueGeneratedOnAdd();
			builder.Property(c => c.Name).IsRequired();
			builder.HasMany(c => c.Products).WithOne(p => p.Category);
		}
    }
}
