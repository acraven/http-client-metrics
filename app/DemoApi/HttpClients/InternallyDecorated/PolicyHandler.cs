using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace DemoApi.HttpClients.InternallyDecorated
{
   public class PolicyHandler : DelegatingHandler
   {
      private readonly IAsyncPolicy<HttpResponseMessage> _policy;

      public PolicyHandler(IAsyncPolicy<HttpResponseMessage> policy)
      {
         _policy = policy ?? throw new ArgumentNullException(nameof(policy));
      }

      protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         var response = await _policy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken).ConfigureAwait(false);
         return response;
      }
   }
}