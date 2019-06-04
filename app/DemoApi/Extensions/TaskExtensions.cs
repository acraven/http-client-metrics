using System;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApi.Extensions
{
   public static class TaskExtensions
   {
      public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
      {
         var cancellationTaskSource = new TaskCompletionSource<bool>();

         using (cancellationToken.Register(s => s.TrySetResult(true), cancellationTaskSource))
         {
            if (task != await Task.WhenAny(task, cancellationTaskSource.Task))
            {
               throw new OperationCanceledException(cancellationToken);
            }

            await task;
         }
      }
   }
}