using Grouchy.Abstractions;
using Microsoft.Extensions.Logging;

namespace Grouchy.Metrics
{
   public class LoggingMetricWriter : IMetricWriter
   {
      private readonly ILogger<LoggingMetricWriter> _logger;

      public LoggingMetricWriter(ILogger<LoggingMetricWriter> logger)
      {
         _logger = logger;
      }

      public void Write(IMetric metric)
      {
         _logger.LogInformation("{@metric}", metric);
      }
   }
}