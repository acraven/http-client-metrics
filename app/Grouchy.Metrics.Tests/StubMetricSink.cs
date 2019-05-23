using System.Collections.Generic;
using Grouchy.Abstractions;

namespace Grouchy.Metrics.Tests
{
   public class StubMetricSink : IMetricSink
   {
      public IList<IMetric> Metrics { get; } = new List<IMetric>();
      
      public void Push(IMetric metric)
      {
         Metrics.Add(metric);
      }
   }
}