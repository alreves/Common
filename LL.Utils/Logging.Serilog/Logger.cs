using System;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Tgc2e.Common.Configuration;

namespace Tgc2e.Logging.SerilogProvider
{
	public class Logger : ILogger
	{
		public Logger()
		{
			var configuration = ConfigurationLoader.Configuration;
			var loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);
			Serilog.Log.Logger = loggerConfiguration.CreateLogger();
		}

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			if (!IsEnabled(logLevel))
			{
				return;
			}

			var message = formatter(state, exception);
			var internalLevel = ConvertLogLevel(logLevel);
			Serilog.Log.Logger.Write(internalLevel, exception, message);
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			var internalLevel = ConvertLogLevel(logLevel);
			return Serilog.Log.Logger.IsEnabled(internalLevel);
		}

		private LogEventLevel ConvertLogLevel(LogLevel logLevel)
		{
			return logLevel switch
			{
				LogLevel.Trace => LogEventLevel.Verbose,
				LogLevel.Debug => LogEventLevel.Debug,
				LogLevel.Information => LogEventLevel.Information,
				LogLevel.Warning => LogEventLevel.Warning,
				LogLevel.Error => LogEventLevel.Error,
				LogLevel.Critical => LogEventLevel.Fatal,
				LogLevel.None => throw new NotSupportedException($"This level {logLevel} not supported"),
				_ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
			};
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			//TODO: тут решить надо со сокупом, нужно ли заморачиваться с реализацией
			return null;
			//return LogContext.PushProperty("OrderId", 1234);
		}

		public void CloseAndFlush()
		{
			Serilog.Log.CloseAndFlush();
		}
	}
}
