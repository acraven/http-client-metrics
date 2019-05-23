using System.Collections.Generic;

namespace Grouchy.Abstractions
{
   public class Gauge : IMetric
   {
      public string Name { get; set; }

      public IDictionary<string, object> Dimensions { get; set; } = new Dictionary<string, object>();

      public double Value { get; set; }
   }
}