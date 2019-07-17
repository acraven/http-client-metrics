using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grouchy.Abstractions;
using Grouchy.Polly.RateLimit;

namespace DemoApi.HttpClients.InternallyDecorated
{
   public class MetricsHandler : DelegatingHandler
   {
      private readonly ITimingBlockFactory _timingBlockFactory;

      public MetricsHandler(ITimingBlockFactory timingBlockFactory)
      {
         _timingBlockFactory = timingBlockFactory;
      }

      protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         var timingBlock = _timingBlockFactory.Create("http_client");
         timingBlock.Dimensions.Add("type", "internally-decorated");

         var response = await timingBlock.ExecuteAsync(() => base.SendAsync(request, cancellationToken)).ConfigureAwait(false);
         return response;
      }
   }
}