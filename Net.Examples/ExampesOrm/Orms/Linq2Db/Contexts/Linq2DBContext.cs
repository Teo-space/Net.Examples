using ExampesOrm.Models;
using LinqToDB.Configuration;


namespace Net.OrmTests.Orms.Linq2Db.Contexts
{
	public class Linq2DBContext : DataContextExtended
	{
        //public Linq2DBContext() : base(ProviderName.SQLite, $"Data Source=InMemorySample;Mode=Memory;Cache=Shared;Version=3;")
        //public Linq2DBContext() : base(ProviderName.SQLite, $"Data Source=Linq2DBContext.db;Version=3;")
        public Linq2DBContext() : base(ProviderName.PostgreSQL,
            //"User ID=postgres;Password=3p33c7u7s6;Host=localhost;Port=5432;Database=postgres;providerName=PostgreSQL"
@"Host=localhost;
Port=5433;
Database=postgres;
Username=postgres;
Password=postgres"
            )
        {
		}
        //Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=здесь_указывается_пароль_от_postgres"

        /*
		public Linq2DBContext(string ProviderName, string ConnectionString) : base(ProviderName, ConnectionString)
		{
		}


        public Linq2DBContext(LinqToDBConnectionOptions options) : base(options.ProviderName, options.ConnectionString)
		{
		}
		*/

        public ITable<Product> Products { get => this.GetTable<Product>(); }
		public ITable<Category> Categories { get => this.GetTable<Category>(); }


	}
}
