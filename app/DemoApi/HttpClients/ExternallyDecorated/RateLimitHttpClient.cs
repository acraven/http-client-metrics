using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grouchy.Polly.RateLimit;

namespace DemoApi.HttpClients.ExternallyDecorated
{
   public class RateLimitHttpClient : IHttpClient
   {
      private readonly IHttpClient _httpClient;
      private readonly AsyncRateLimitPolicy<HttpResponseMessage> _rateLimitPolicy;

      public RateLimitHttpClient(
         IHttpClient httpClient,
         IRateLimiter rateLimiter)
      {
         _httpClient = httpClient;

         _rateLimitPolicy = new AsyncRateLimitPolicy<HttpResponseMessage>(rateLimiter, (context, exception) => Task.CompletedTask);
      }

      public string Name => _httpClient.Name;

      public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         var response = await _rateLimitPolicy.ExecuteAsync(() => _httpClient.SendAsync(request, cancellationToken));
         return response;
      }
   }
}