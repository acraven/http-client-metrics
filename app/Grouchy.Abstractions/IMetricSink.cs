namespace Grouchy.Abstractions
{
   public interface IMetricSink
   {
      void Push(IMetric metric);
   }
}