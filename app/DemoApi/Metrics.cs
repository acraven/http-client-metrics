using System;
using System.Threading.Tasks;

namespace DemoApi
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
   public interface IMetricFactory
   {
      ITimingMetric CreateTimingMetric(string name);
   }
   
   public interface IMetric : IDisposable
   {
      string Name { get; }

      void AddDimension(string key, object value);
   }
   
   public interface ITimingMetric : IMetric, IDisposable
   {
      int Duration { get; }
   }

   public class MetricFactory : IMetricFactory
   {
      public ITimingMetric CreateTimingMetric(string name)
      {
         return new TimingMetric(name);
      }
   }

   public class TimingMetric : IMetric
   {
      
   }
   
   public interface IMetricWriter
   {
      void Write(IMetric metric);
   }
   
   public class MetricWriter : IMetricWriter
   {
      public void Write(IMetric metric)
      {
         throw new NotImplementedException();
      }
   }
}