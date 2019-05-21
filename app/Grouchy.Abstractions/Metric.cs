using System.Collections.Generic;

namespace Grouchy.Abstractions
{
   public class Metric
   {
      public string Name { get; set; }

      public IDictionary<string, object> Dimensions { get; } = new Dictionary<string, object>();
   }
}