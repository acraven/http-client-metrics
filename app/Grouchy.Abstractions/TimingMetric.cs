using System;

namespace Grouchy.Abstractions
{
   public class ITimingMetric : Metric, IDisposable
   {
      int Duration { get; }
   }
}