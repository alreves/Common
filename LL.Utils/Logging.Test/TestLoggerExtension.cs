using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tgc2e.Logging;
using ILogger = Tgc2e.Logging.ILogger;

namespace Logging.Test
{
	[TestClass]
	public class TestLoggerExtension
	{
		private readonly LoggingCategory _category = new("TestCategory");
		private const string LogsDir = "Logs";

		[TestMethod]
		public void TestLogLevels()
		{
			var testedLevels = new Dictionary<LogLevel, Action<ILogger, LoggingCategory, string, object>>()
			{
				{LogLevel.Debug, LoggerExtension.Debug},
				{LogLevel.Information, LoggerExtension.Info},
				{LogLevel.Error, LoggerExtension.Error},
				{LogLevel.Trace, LoggerExtension.Trace},
				{LogLevel.Warning, LoggerExtension.Warn},
				{LogLevel.Critical, LoggerExtension.Critical}
			};

			foreach (var testedLevel in testedLevels)
			{
				var testResult = TestLog(testedLevel.Key, testedLevel.Value);
				Assert.IsTrue(testResult, $"LogLevel [{testedLevel.Key}] check failed");
			}
		}

		[TestMethod]
		public void TestRunMethod()
		{
			var logData = RunLoggerActionAndGetResult((logger) => logger.LogMethodRequested(_category, this));
			var desiredString = $"[DBG] (App=Logger.Test/Mx={Environment.MachineName}/Td={Thread.CurrentThread.ManagedThreadId}) [{_category.CategoryName}]:TestRunMethod: REQUESTED..., [{GetType().FullName}]";
			Assert.IsTrue(logData.EndsWith(desiredString));
		}

		private string RunLoggerActionAndGetResult(Action<ILogger> action)
		{
			Clear();
			ILogger logger = new Tgc2e.Logging.SerilogProvider.Logger();
			action(logger);
			logger.CloseAndFlush();
			return GetLogValue();
		}

		private bool CheckResult(string logData, string message, LogLevel level)
		{
			var logLevelPresentation = string.Empty;
			switch (level)
			{
				case LogLevel.Trace:
					logLevelPresentation = "VRB";
					break;
				case LogLevel.Debug:
					logLevelPresentation = "DBG";
					break;
				case LogLevel.Information:
					logLevelPresentation = "INF";
					break;
				case LogLevel.Warning:
					logLevelPresentation = "WRN";
					break;
				case LogLevel.Error:
					logLevelPresentation = "ERR";
					break;
				case LogLevel.Critical:
					logLevelPresentation = "FTL";
					break;
				case LogLevel.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(level), level, null);
			}

			var desiredString = $"[{logLevelPresentation}] (App=Logger.Test/Mx={Environment.MachineName}/Td={Thread.CurrentThread.ManagedThreadId}) [{_category.CategoryName}]:{message}, [{this.GetType().FullName}]";
			return logData.EndsWith(desiredString);

		}

		private void Clear()
		{
			if (Directory.Exists(LogsDir))
			{
				var files = Directory.GetFiles(LogsDir);
				foreach (var file in files)
				{
					File.Delete(file);
				}

				Directory.Delete(LogsDir);
			}
		}

		private string GetLogValue()
		{
			var files = Directory.GetFiles(LogsDir);
			if (files.Length > 1)
			{
				throw new InvalidOperationException("Can be no more than 1 log file");
			}

			return File.ReadAllText(files[0]).Trim();
		}

		public bool TestLog(LogLevel level, Action<ILogger, LoggingCategory, string, object> logAction)
		{
			string message = $"{level} message";
			var logData = RunLoggerActionAndGetResult((logger) => logAction(logger, _category, message, this));
			return CheckResult(logData, message, level);
		}
	}
}