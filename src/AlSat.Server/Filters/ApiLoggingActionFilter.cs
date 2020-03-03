
using System.Diagnostics;
using System.Reflection;

using AlSat.Server.Controllers;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using NLog;

namespace AlSat.Server.Filters
{
	public class ApiLoggingActionFilter : IActionFilter
	{
		private readonly ILogger<ApiLoggingActionFilter> mLogger = null;
		//private const string StopwatchKey = "Stopwatch";
		Stopwatch mStopwatch = new Stopwatch();

		public ApiLoggingActionFilter(ILogger<ApiLoggingActionFilter> logger)
		{
			mLogger = logger;
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			mStopwatch.Start();

			if (context.Controller is BaseController controller)
			{
				MappedDiagnosticsContext.Set("controller", controller.ControllerContext.ActionDescriptor.ControllerName);
				MappedDiagnosticsContext.Set("action", controller.ControllerContext.ActionDescriptor.ActionName);
			}

			string loggerMethod = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

			var logger = LogManager.GetCurrentClassLogger();

			LogEventInfo info = new LogEventInfo(NLog.LogLevel.Trace, loggerMethod, "Action Executing");

			logger.Log(info);
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			mStopwatch.Stop();

			var logger = LogManager.GetCurrentClassLogger();

			string loggerMethod = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

			LogEventInfo info = new LogEventInfo(NLog.LogLevel.Trace, loggerMethod, "Action Executed");
			info.Properties["TimeElapsed"] = mStopwatch.ElapsedMilliseconds;

			logger.Log(info);
		}
	}
}
