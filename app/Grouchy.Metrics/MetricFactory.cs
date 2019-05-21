using DemoApi;
using Grouchy.Abstractions;

namespace Grouchy.Metrics
{
//   public static class Metrics2
//   {
//      public static async Task MeasureAsync(Func<Task> measureThis)
//      {
//         // TODO: Add timing and record metrics
//         await measureThis();
//      }
//   }
//

   public class MetricFactory : IMetricFactory
   {
      public ITimingMetric CreateTimingMetric(string name)
      {
         return new TimingMetric(name);
      }
   }
}