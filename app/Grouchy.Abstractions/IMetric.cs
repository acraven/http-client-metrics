using System.Collections.Generic;

namespace Grouchy.Abstractions
{
   public interface IMetric
   {
      string Name { get; }

      IDictionary<string, object> Dimensions { get; }
   }
}