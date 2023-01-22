namespace OrmAndLinq.dbo.Linq2Db.MappingConfigurations
{
	public class MappingConfiguration<T>
	{
		public virtual void Configure(DataContext dataContext)
		{
			//print($"[{this.GetType().Name}] {MethodBase.GetCurrentMethod().Name}");
		}

		public MappingConfiguration<T> Create() => new MappingConfiguration<T>();

		public static MappingConfiguration<T> CreateInstance() => new MappingConfiguration<T>();
		public static MappingConfiguration<TConfig> Create<TConfig>() => new MappingConfiguration<TConfig>();

	}

}
