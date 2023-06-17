public static class ExtensionsIEnumerable
{
	public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source) where TSource : class
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}

		return source.Where(x => x != default);
	}

	public static IEnumerable<string> WhereNotEmpty(this IEnumerable<string> source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}

		return source.Where(x => !string.IsNullOrEmpty(x));
	}

}