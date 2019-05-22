using System.Linq;
using NUnit.Framework;

namespace Grouchy.Metrics.Tests.SimpleMetricScenarios
{
   public class happy_path
   {
      [SetUp]
      public void Setup()
      {
      }

      [Test]
      public void Test1()
      {
         var logger = new StubLogger<LoggingMetricWriter>();
         var m = new LoggingMetricWriter(logger);

         var metric = new Metric {Name = "foo"};
         m.Write(metric);

         Assert.That(logger.Entries.Single().Values["@metric"], Is.SameAs(metric));
      }
   }
}