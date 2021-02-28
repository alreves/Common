using Microsoft.AspNetCore.Mvc;
using Tgc2e.Logging;

namespace Tgc2e.Common.Web.Api
{
	public abstract class ControllerWithLoggingBase : ControllerBase
	{
		protected ILogger Logger
		{
			get;
		}

		public LoggingCategory Category { get; }

		protected ControllerWithLoggingBase(ILogger logger, LoggingCategory category)
		{
			Logger = logger;
			Category = category;
		}
	}
}
