namespace Tgc2e.Logging
{
	public interface ILogger : Microsoft.Extensions.Logging.ILogger
	{
		void CloseAndFlush();
	}
}