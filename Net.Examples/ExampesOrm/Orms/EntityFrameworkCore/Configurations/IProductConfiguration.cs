using ExampesOrm.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Net.OrmTests.Orms.EntityFrameworkCore.Configurations
{
	public class IProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.ToTable("Products");
			builder.HasIndex(p => p.ProductId);
			builder.Property(p => p.ProductId)//.ValueGeneratedOnAdd()
				;
			builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).IsRequired();

            builder.HasOne(p => p.Category).WithMany(o => o.Products);
		}
	}
}
