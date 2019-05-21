namespace Grouchy.Abstractions
{
   public interface IMetricFactory
   {
      ITimingMetric CreateTimingMetric(string name);
   }
}