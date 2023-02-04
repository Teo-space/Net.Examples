using ExampesOrm.Models;
using OrmAndLinq.dbo.Linq2Db.MappingConfigurations;


namespace Net.OrmTests.Orms.Linq2Db.Configurations
{
	public class ICategoryConfiguration : MappingConfiguration<Category>
	{
		public override void Configure(DataContext dataContext)
		{
			dataContext
				.GetFluentMappingBuilder()
				.Entity<Category>()
				.HasTableName("Categories")

				.HasPrimaryKey(category => category.CategoryId)//.HasIdentity(category => category.CategoryId)

				.HasColumn(category => category.CategoryId)
				.HasColumn(category => category.Name)
				
				.Association(category => category.Products, category => category.CategoryId, product => product.CategoryId)
				;
		}
	}

}
