using ExampesOrm.Models;
using LinqToDB.Configuration;


namespace Net.OrmTests.Orms.Linq2Db.Contexts
{
	public class Linq2DBContext : DataContextExtended
	{
		public static string SQLiteFilePath { get; } = "Linq2dbDataConnection.db";


		public Linq2DBContext() : base(ProviderName.SQLite, $"Data Source={SQLiteFilePath};Version=3;")
		//public Linq2DBContext() : base(ProviderName.SQLite, $"Data Source=InMemorySample;Mode=Memory;Cache=Shared;Version=3;")
		{
		}
		public Linq2DBContext(string ProviderName, string ConnectionString) : base(ProviderName, ConnectionString)
		{
		}
		public Linq2DBContext(LinqToDBConnectionOptions options) : base(options.ProviderName, options.ConnectionString)
		{
		}


		public ITable<Product> Products { get => this.GetTable<Product>(); }
		public ITable<Category> Categories { get => this.GetTable<Category>(); }


	}
}
