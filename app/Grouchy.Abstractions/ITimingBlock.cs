using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grouchy.Abstractions
{
   public interface ITimingBlock
   {
      IDictionary<string, object> Dimensions { get; }

      Task ExecuteAsync(Func<Task> action);

      Task<T> ExecuteAsync<T>(Func<Task<T>> action);
   }
}