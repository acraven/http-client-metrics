using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace Grouchy.Polly.RateLimit
{
   public interface IRateLimiter
   {
      Task WaitAsync(Context context, CancellationToken cancellationToken);
   }
}