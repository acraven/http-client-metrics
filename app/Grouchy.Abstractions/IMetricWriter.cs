namespace Grouchy.Abstractions
{
   public interface IMetricWriter
   {
      void Write(IMetric metric);
   }
}