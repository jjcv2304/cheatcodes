using System;
using Microsoft.Extensions.Logging;

namespace Api.Logs.Filters
{
  public static class LogMessages
  {
    private static readonly Action<ILogger, string, string, long, Exception> _routePerformance;

    static LogMessages()
    {
      _routePerformance = LoggerMessage.Define<string, string, long>(LogLevel.Warning, 0,
        "{RouteName} {Method} code took {ElapsedMilliseconds}.");
    }

    public static void LogRoutePerformance(this ILogger logger, string pageName, string method,
      long elapsedMilliseconds)
    {
      _routePerformance(logger, pageName, method, elapsedMilliseconds, null);
    }
  }
}