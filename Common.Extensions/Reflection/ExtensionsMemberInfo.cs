using MoreLinq;

public static class ExtensionsMemberInfo
{

	public static string ToStringMembers<T>(this T o)
	{
		if (o == null) return "\n[ToStringMembers] o == default";
		return o.ToStringMethods() + o.ToStringProperties() + o.ToStringFields();
	}

	static List<string> ObjectMethods = typeof(object).GetMethods().Select(om => om.Name).ToList();

	public static string ToStringMethods<T>(this T o)
	{
		if (o == null) return "\n[ToStringMethods] o == default";
		try
		{
			var type = o.GetType();
			var props = type
				.GetMethods()
				.Where(m => !ObjectMethods.Contains(m.Name))
				.Select(m => $"{m?.Name}:    Parameters: {m.GetParameters().Length}")
				.ToDelimitedString("\n")
				;
			return $"\n[ToStringMethods: {o.GetType()?.Name}]\n{props}";
		}
		catch (Exception ex)
		{
			return $"\n[ToStringMethods: {o?.GetType()?.Name}]\n{ex.Message}";
		}
	}




	public static string ToStringProperties<T>(this T o)// where T : class
	{
		if (o == null) return "\n[ToStringProperties] o == default";
		try
		{
			var type = o.GetType();
			var props = type
				.GetProperties()
				.Select(x =>
				{
					var m = x?.GetMethod;
					string res = string.Empty;
					try
					{
						res = x?.GetMethod.Invoke(o, null)?.ToString();
					}
					catch (Exception ex)
					{
						res = $"Exception: {ex.Message}";
					}
					return m == default ? $"{x?.Name} get method is null" : $"{x?.Name}:    {res}";
				})
				.ToDelimitedString("\n")
				;
			return $"\n[ToStringProperties: {o.GetType()?.Name}]\n{props}";
		}
		catch (Exception ex)
		{
			return $"\n[ToStringProperties: {o?.GetType()?.Name}]\n{ex.Message}";
		}

	}


	public static string ToStringFields<T>(this T o)
	{
		if (o == null) return "\n[ToStringFields] o == default";
		try
		{
			var type = o.GetType();
			var props = type
				.GetFields()
				.Select(x =>
				{
					string res = string.Empty;
					try
					{
						res = x?.GetValue(o)?.ToString();
					}
					catch (Exception ex)
					{
						res = $"Exception: {ex.Message}";
					}
					return $"{x?.Name}:    {res}";
				})
				.ToDelimitedString("\n")
				;
			return $"\n[ToStringFields: {o.GetType()?.Name}]\n{props}";
		}
		catch (Exception ex)
		{
			return $"\n[ToStringFields: {o?.GetType()?.Name}]\n{ex.Message}";
		}

	}


}