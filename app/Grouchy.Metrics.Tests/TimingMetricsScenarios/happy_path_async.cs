using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grouchy.Abstractions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Grouchy.Metrics.Tests.TimingMetricsScenarios
{
   public class happy_path_async
   {
      private StubMetricSink _metricSink;

      [OneTimeSetUp]
      public async Task setup_once_before_all_tests()
      {
         _metricSink = new StubMetricSink();

         var timingBlockFactory = new TimingBlockFactory(_metricSink);

         using (var timingBlock = timingBlockFactory.Create("foo"))
         {
            await timingBlock.ExecuteAsync(() => Task.Delay(200));
         }
      }

      [Test]
      public void should_write_two_metrics()
      {
         Assert.That(_metricSink.Metrics.Count, Is.EqualTo(2));
      }
      
      [Test]
      public void Test1()
      {
         var logger = new StubLogger<LoggingMetricSink>();
         var m = new LoggingMetricSink(logger);

         var metric = new Metric {Name = "foo"};
         m.Push(metric);

         Assert.That(logger.Entries.Single().Values["@metric"], Is.SameAs(metric));
      }
   }

   public class StubMetricSink : IMetricSink
   {
      public IList<IMetric> Metrics { get; } = new List<IMetric>();
      
      public void Push(IMetric metric)
      {
         Metrics.Add(metric);
      }
   }
}