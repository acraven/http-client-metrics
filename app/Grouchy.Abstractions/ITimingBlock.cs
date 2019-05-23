using System;
using System.Threading.Tasks;

namespace Grouchy.Abstractions
{
   public interface ITimingBlock
   {
      Task ExecuteAsync(Func<Task> action);
   }
}