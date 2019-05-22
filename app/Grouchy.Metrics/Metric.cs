using System.Collections.Generic;
using Grouchy.Abstractions;

namespace Grouchy.Metrics
{
   public class Metric : IMetric
   {
      public string Name { get; set; }

      public IDictionary<string, object> Dimensions { get; } = new Dictionary<string, object>();
   }
}