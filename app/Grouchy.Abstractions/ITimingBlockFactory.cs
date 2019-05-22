using System;
using System.Threading.Tasks;

namespace Grouchy.Abstractions
{
   public interface ITimingBlockFactory
   {
      ITimingBlock Create(string name);
   }

   public interface ITimingBlock : IDisposable
   {
      //void AddDimension

      Task ExecuteAsync(Func<Task> action);
   }
}