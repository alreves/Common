using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Tgc2e.Logging
{
	public static class LoggerExtension
	{
		public static void Debug(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Debug, category, message, null, null, source);
		}

		public static void Info(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Information, category, message, null, null, source);
		}

		public static void Warn(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Warning, category, message, null, null, source);
		}

		public static void Trace(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Trace, category, message, null, null, source);
		}

		public static void Error(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Error, category, message, null, null, source);
		}

		public static void Error(this ILogger logger, LoggingCategory category, Exception ex, string message, object source = null)
		{
			Log(logger, LogLevel.Error, category, message, ex, null, source);
		}

		public static void Critical(this ILogger logger, LoggingCategory category, string message, object source = null)
		{
			Log(logger, LogLevel.Critical, category, message, null, null, source);
		}

		public static void LogMethodRequested(this ILogger logger, LoggingCategory loggingCategory, object source = null, string additionalData = null, [CallerMemberName] string methodName = null)
		{
			var messageToWrite = additionalData != null
				? methodName + ": REQUESTED... (" + additionalData + ")"
				: methodName + ": REQUESTED...";

			Debug(logger, loggingCategory, messageToWrite, source);
		}

		public static void LogMethodCompleted(this ILogger logger, LoggingCategory loggingCategory, object source = null, string additionalData = null, [CallerMemberName] string methodName = null)
		{
			var messageToWrite = additionalData != null
				? methodName + ": COMPLETED. (" + additionalData + ")"
				: methodName + ": COMPLETED.";

			Debug(logger, loggingCategory, messageToWrite, source);
		}

		public static void LogMethodFailed(this ILogger logger, LoggingCategory loggingCategory, object source = null, string additionalData = null, [CallerMemberName] string methodName = null)
		{
			var messageToWrite = additionalData != null
				? methodName + ": FAILED! (" + additionalData + ")"
				: methodName + ": FAILED!";

			Debug(logger, loggingCategory, messageToWrite, source);
		}

		private static void Log(this ILogger logger, LogLevel level, LoggingCategory category, string message, Exception exception, object[] args, object source)
		{
			if (logger == null) return;

			var messageToWrite = new StringBuilder();
			messageToWrite.Append($"[{category.CategoryName}]:");
			messageToWrite.Append(message);

			if (source != null)
			{
				messageToWrite.Append($", [{source}]");
			}

			if (exception == null)
			{
				logger.Log(level, messageToWrite.ToString(), args);
			}
			else
			{
				logger.Log(level, exception, messageToWrite.ToString(), args);
			}
		}
	}
}