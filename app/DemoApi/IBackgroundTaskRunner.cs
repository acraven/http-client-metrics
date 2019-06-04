using System.Threading.Tasks;

namespace DemoApi
{
   public interface IBackgroundTaskRunner
   {
      void Execute(Task action);
   }
}