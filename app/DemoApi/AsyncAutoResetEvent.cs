using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DemoApi.Extensions;

namespace DemoApi
{
   public class AsyncAutoResetEvent
   {
      private static readonly Task Completed = Task.FromResult(true);

      private readonly Queue<TaskCompletionSource<bool>> _waiters = new Queue<TaskCompletionSource<bool>>();

      private bool _signaled;

      public Task WaitAsync()
      {
         return WaitAsync(t => t);
      }

      public Task WaitAsync(CancellationToken cancellationToken)
      {
         return WaitAsync(t => t.WithCancellation(cancellationToken));
      }

      private Task WaitAsync(Func<Task, Task> taskWrapper)
      {
         lock (_waiters)
         {
            if (_signaled)
            {
               _signaled = false;
               return Completed;
            }

            var waitingTaskSource = new TaskCompletionSource<bool>();
            _waiters.Enqueue(waitingTaskSource);

            return taskWrapper(waitingTaskSource.Task);
         }
      }

      public void Set()
      {
         TaskCompletionSource<bool> releasedTaskSource = null;

         lock (_waiters)
         {
            if (_waiters.Count > 0)
               releasedTaskSource = _waiters.Dequeue();
            else if (!_signaled)
               _signaled = true;
         }

         releasedTaskSource?.SetResult(true);
      }
   }
}