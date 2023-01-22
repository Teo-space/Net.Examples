using ExampesOrm.Models;
using OrmAndLinq.dbo.Linq2Db.MappingConfigurations;



namespace Net.OrmTests.Orms.Linq2Db.Configurations;


class IProductConfiguration : MappingConfiguration<Product>
{
	public override void Configure(DataContext dataContext)
	{

		dataContext
			.GetFluentMappingBuilder()
			.Entity<Product>()
			.HasTableName("Products")
			.HasPrimaryKey(product => product.ProductId).HasIdentity(product => product.ProductId)

			.HasColumn(product => product.ProductId)
			.HasColumn(product => product.Name)
			.HasColumn(product => product.CategoryId)

			.Association(	product => product.Category, 
							product => product.CategoryId, 
							category => category.CategoryId)
			;
	}
}
