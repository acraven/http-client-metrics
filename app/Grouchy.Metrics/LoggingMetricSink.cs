using Grouchy.Abstractions;
using Microsoft.Extensions.Logging;

namespace Grouchy.Metrics
{
   public class LoggingMetricSink : IMetricSink
   {
      private readonly ILogger<LoggingMetricSink> _logger;

      public LoggingMetricSink(ILogger<LoggingMetricSink> logger)
      {
         _logger = logger;
      }

      public void Push(IMetric metric)
      {
         _logger.LogInformation("{@metric}", metric);
      }
   }
}