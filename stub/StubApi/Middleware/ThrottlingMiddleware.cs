using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StubApi.Middleware
{
   public class ThrottlingMiddleware
   {
      private const int ConcurrentRequests = 10;

      private readonly RequestDelegate _next;
      private readonly SemaphoreSlim _semaphore;

      public ThrottlingMiddleware(RequestDelegate next)
      {
         _next = next;
         _semaphore = new SemaphoreSlim(ConcurrentRequests);
      }

      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _semaphore.WaitAsync(context.RequestAborted).ConfigureAwait(false);

            await _next(context);
         }
         finally
         {
            _semaphore.Release();
         }
      }
   }
}