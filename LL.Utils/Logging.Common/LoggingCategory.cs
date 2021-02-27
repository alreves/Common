namespace Tgc2e.Logging
{
	public readonly struct LoggingCategory
	{
		public static LoggingCategory DefaultCategory = new LoggingCategory("*");

		public string CategoryName { get; }

		public LoggingCategory(string categoryName)
		{
			CategoryName = categoryName;
		}
	}
}