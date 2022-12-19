using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace PROWeb.Common.Extensions
{
    public static class LoggerExtensions
    {
        public static void Error(this ILogger logger, Exception exception)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame? stackFrame = stackTrace.GetFrame(1);

            logger.Error(exception, $"Method: {stackFrame?.GetMethod()?.Name}");
        }
    }
}
