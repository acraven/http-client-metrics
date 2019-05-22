using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DemoApi;
using Grouchy.Abstractions;
using Polly;

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

   public class TimingBlockFactory : ITimingBlockFactory
   {
      private readonly IMetricSink _metricSink;

      public TimingBlockFactory(IMetricSink metricSink)
      {
         _metricSink = metricSink;
      }
      
      public ITimingBlock Create(string name)
      {
         return new TimingBlock(name, _metricSink);
      }
   }

   public class TimingBlock : ITimingBlock, IDisposable
   {
      private readonly string _name;
      private readonly IMetricSink _metricSink;

      public TimingBlock(string name, IMetricSink metricSink)
      {
         _name = name;
         _metricSink = metricSink;
      }

      public void Dispose()
      {
      }

      public async Task ExecuteAsync(Func<Task> action)
      {
         _metricSink.Push(new Metric {Name = _name});

         var stopwatch = Stopwatch.StartNew();

         try
         {
            await action();
         }
         catch (Exception e)
         {
//TODO:            WithException(e);
            throw;
         }
         finally
         {
            stopwatch.Stop();
            
            _metricSink.Push(new Metric {Name = _name});
        }
      }
   }
}