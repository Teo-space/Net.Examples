using LinqToDB;
using MoreLinq;
using OrmAndLinq.dbo.Linq2Db.MappingConfigurations;
using System.Collections.Concurrent;


namespace Net.OrmTests.Orms.Linq2Db.Contexts
{
	public class DataContextExtended : DataContext
	{


		public static bool Debug = false;
		static DataContextExtended()
		{
			/*
			LinqToDB.Data.DataConnection.TurnTraceSwitchOn();
			LinqToDB.Data.DataConnection.WriteTraceLine = (message, displayName, trace) =>
			{
				if (Debug)
				{
					print(message);
					print(displayName);
				}
			};*/
		}





		public DataContextExtended EnableDebug()
		{
			Debug = true;
			return this;
		}

		public DataContextExtended DisableDebug()
		{
			Debug = false;
			return this;
		}


		public DataContextExtended(string providerName, string connectionString) : base(providerName, connectionString)
		{
			if (!MapCurrentAssemblyCalled)
			{
				MapCurrentAssemblyCalled = true;
				this.MapCurrentAssembly();
			}
			if (!CreateTablesCalled)
			{
				CreateTablesCalled = true;
				this.CreateTables();
			}
		}


		bool MapCurrentAssemblyCalled = false;
		bool CreateTablesCalled = false;



		static bool CurrentAssemblyIsMapped = false;
		public void MapCurrentAssembly()
		{
			if (CurrentAssemblyIsMapped) return;
			CurrentAssemblyIsMapped = true;

			MapAssembly(Assembly.GetExecutingAssembly());
		}


		static ConcurrentDictionary<Assembly, bool> MappedAssemblies = new();
		public void MapAssembly(Assembly assembly)
		{
			if (MappedAssemblies.TryGetValue(assembly, out bool isMapped)) return;
			MappedAssemblies[assembly] = true;
			assembly
				.GetTypes()
				.Where(t => t.BaseType != default && t.BaseType.IsGenericType)
				.Where(t => t.BaseType.GetGenericTypeDefinition() == typeof(MappingConfiguration<>))
				.Select(t => t.GetConstructors().Where(c => c.GetParameters().Length == 0))
				.SelectMany(cs => cs)
				.Select(c => c.Invoke(null))
				.Where(Configuration => Configuration != null)
				.Pipe(Configuration => 
				{
					Configuration
					.GetType()
					.GetMethod("Configure")
					.Invoke(Configuration, new object[] { this });
				})
				.ToList()
				;
		}




		public void CreateTables()
		{
			var CreateTable = typeof(DataExtensions).GetMethod("CreateTable");

			this.GetType()
				.GetProperties()
				.Where(p => p.DeclaringType == this.GetType())
				.Where(p => p.PropertyType != default && p.PropertyType.IsGenericType)
				.Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(ITable<>))
				.ToList()
				.ForEach(p =>
				{
                    try
                    {
                        MethodInfo generic = CreateTable.MakeGenericMethod(p.PropertyType.GetGenericArguments());

                        var mparams = new object[generic.GetParameters().Length];
                        mparams[0] = this;
                        for (int i = 1; i < generic.GetParameters().Length; i++)
                        {
                            mparams[i] = Type.Missing;
                        }

                        generic.Invoke(null, mparams);
                    }
                    catch (Exception ex)//already exists
                    {
                        if (!string.IsNullOrEmpty(ex?.InnerException?.Message) && ex.InnerException.Message.Contains("already exists"))
                        {
                            return;
                        }
                    }
                })
				;
		}


		protected ITable<T> GetOrCreateTable<T>() where T : class
		{
			try
			{
				return this.CreateTable<T>();
			}
			catch
			{
				return this.GetTable<T>();
			}
		}

	}



}
