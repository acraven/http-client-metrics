using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DemoApi.BackgroundServices
{
   public class TaskRunnerBackgroundService : BackgroundService, IBackgroundTaskRunner
   {
      private readonly ConcurrentDictionary<int, Task> _outstandingTasks = new ConcurrentDictionary<int, Task>();
      private readonly AsyncAutoResetEvent _autoResetEvent = new AsyncAutoResetEvent();

      public void Execute(Task action)
      {
         // TODO: What if this fails?
         if (_outstandingTasks.TryAdd(action.Id, action))
         {
            _autoResetEvent.Set();
         }
      }

      protected override async Task ExecuteAsync(CancellationToken stoppingToken)
      {
         while (!stoppingToken.IsCancellationRequested)
         {
            await _autoResetEvent.WaitAsync(stoppingToken);

            try
            {
               var tasks = _outstandingTasks.Values.ToArray();

               var index = Task.WaitAny(tasks, stoppingToken);

               _outstandingTasks.TryRemove(tasks[index].Id, out _);
            }
            catch (OperationCanceledException e) when (e.CancellationToken == stoppingToken)
            {
               break;
            }
            catch (Exception e)
            {
               Console.Error.WriteLine(e);
            }
         }
      }
   }
}