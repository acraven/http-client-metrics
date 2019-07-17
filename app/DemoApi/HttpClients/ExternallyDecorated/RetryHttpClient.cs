using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DemoApi.Extensions;
using Grouchy.Polly.RateLimit;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace DemoApi.HttpClients.ExternallyDecorated
{
   public class RetryHttpClient : IHttpClient
   {
      private readonly IHttpClient _httpClient;
      private AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

      public RetryHttpClient(IHttpClient httpClient)
      {
         _httpClient = httpClient;

         _retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TaskCanceledException>()
            .Or<RateLimitRejectionException>()
            .WaitAndRetryAsync(Constants.DefaultRetryDurationsMs.ToDurations());
      }

      public string Name => _httpClient.Name;

      public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         try
         {
            var response = await _retryPolicy.ExecuteAsync(async ct =>
            {
               var clonedRequest = await CloneRequestAsync(request);
               return await _httpClient.SendAsync(clonedRequest, ct);
            }, cancellationToken).ConfigureAwait(false);
            return response;
         }
         finally
         {
            // Preserve compatibility with HttpClient.SendAsync by disposing of the original request
            request.Content?.Dispose();
         }
      }

      // From http://stackoverflow.com/questions/25044166/how-to-clone-a-httprequestmessage-when-the-original-request-has-content
      private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
      {
         var clone = new HttpRequestMessage(request.Method, request.RequestUri) { Version = request.Version };

         if (request.Content != null)
         {
            // Copy the request's content (via a MemoryStream) into the cloned object
            var ms = new MemoryStream();

            await request.Content.CopyToAsync(ms).ConfigureAwait(false);

            ms.Position = 0;
            clone.Content = new StreamContent(ms);

            if (request.Content.Headers != null)
            {
               // Copy the content headers
               foreach (var header in request.Content.Headers)
               {
                  clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
               }
            }
         }

         // Copy the properties
         foreach (var property in request.Properties)
         {
            clone.Properties.Add(property);
         }

         // Copy the request headers
         foreach (var header in request.Headers)
         {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
         }

         return clone;
      }
   }
}