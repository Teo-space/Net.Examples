using System.Data;

public static class ExtensionsDataTable
{
	public static DataTable ToDataTable<TSource>(this IEnumerable<TSource> items) where TSource : class
	{
		DataTable table = new();

		var props = typeof(TSource).GetProperties()
			.Where(x => x.PropertyType.IsValueType || x.PropertyType.IsPrimitive || x.PropertyType == typeof(string))
			.ToDictionary(x => x.Name);

		if (props.Count == 0)
		{
			throw new InvalidOperationException($"TSource Properties. Sequence contains no elements");
		}

		foreach (var prop in props)
		{
			table.Columns.Add(prop.Key);
		}

		if (items.Count() == 0)
		{
			throw new InvalidOperationException($"TSource items. Sequence contains no elements");
		}

		foreach (var item in items)
		{
			var row = table.NewRow();
			foreach (var prop in props)
			{
				row[prop.Key] = prop.Value.GetValue(item);
			}
			table.Rows.Add(row);
		}

		return table;
	}
}