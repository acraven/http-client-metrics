using System.Collections.Generic;
using System.Linq;
using Grouchy.Abstractions;
using NUnit.Framework;

namespace Grouchy.Metrics.Tests.SimpleMetricScenarios
{
   public class happy_path_with_dimensions
   {
      private StubLogger<LoggingMetricSink> _logger;

      [OneTimeSetUp]
      public void setup_once_before_all_tests()
      {
         _logger = new StubLogger<LoggingMetricSink>();

         var loggingMetricSink = new LoggingMetricSink(_logger);

         var metric = new Counter {Name = "foo", Dimensions = new Dictionary<string, object> {{"a", 1}, {"b", 2}}};
         loggingMetricSink.Push(metric);
      }

      [Test]
      public void should_pass_metric_as_argument_to_logger()
      {
         var metric = (Counter) _logger.Entries.Single().Values["@metric"];

         Assert.That(metric.Name, Is.EqualTo("foo"));
         Assert.That(metric.Dimensions.Keys, Is.EquivalentTo(new[] {"a", "b"}));
         Assert.That(metric.Dimensions.Values, Is.EquivalentTo(new[] {1, 2}));
      }
   }
}