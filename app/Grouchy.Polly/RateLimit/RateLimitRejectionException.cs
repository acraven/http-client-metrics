using Polly;

namespace Grouchy.Polly.RateLimit
{
   public class RateLimitRejectionException : ExecutionRejectedException
   {
   }
}