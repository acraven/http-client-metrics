using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grouchy.Abstractions;

namespace Grouchy.Metrics
{
   public class TimingBlock : ITimingBlock
   {
      private readonly string _name;
      private readonly IMetricSink _metricSink;

      public TimingBlock(string name, IMetricSink metricSink)
      {
         _name = name;
         _metricSink = metricSink;
      }

      public async Task ExecuteAsync(Func<Task> action)
      {
         _metricSink.Push(new Counter
         {
            Name = $"{_name}_start"
         });

         var stopwatch = Stopwatch.StartNew();
         var dimensions = new Dictionary<string, object>();

         try
         {
            await action();
         }
         catch (Exception e)
         {
            dimensions.Add("exception", e.GetType().FullName);

            throw;
         }
         finally
         {
            stopwatch.Stop();

            _metricSink.Push(new Gauge
            {
               Name = $"{_name}_end",
               Dimensions = dimensions,
               Value = stopwatch.ElapsedMilliseconds
            });
         }
      }
   }
}