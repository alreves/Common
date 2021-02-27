using System;
using Microsoft.Extensions.Logging;

namespace Tgc2e.Logging
{
	public static class LoggerExtension
	{
		public static void Debug(this ILogger logger, LoggingCategory category, string message)
		{
			Log(logger, LogLevel.Debug, category, message, null, null);
		}

		public static void Info(this ILogger logger, LoggingCategory category, string message)
		{
			Log(logger, LogLevel.Information, category, message, null, null);
		}

		public static void Warn(this ILogger logger, LoggingCategory category, string message)
		{
			Log(logger, LogLevel.Warning, category, message, null, null);
		}

		public static void Trace(this ILogger logger, LoggingCategory category, string message)
		{
			Log(logger, LogLevel.Trace, category, message, null, null);
		}

		private static void Log(this ILogger logger, LogLevel level, LoggingCategory category, string message, Exception exception, object[] args)
		{
			if (logger == null) return;

			string messageToWrite = $"{category.CategoryName}: {message}";

			if (exception == null)
			{
				logger.Log(level, messageToWrite, args);
			}
			else
			{
				logger.Log(level, exception, messageToWrite, args);
			}
		}
	}
}