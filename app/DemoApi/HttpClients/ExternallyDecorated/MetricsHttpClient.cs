using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grouchy.Abstractions;

namespace DemoApi.HttpClients.ExternallyDecorated
{
   public class MetricsHttpClient : IHttpClient
   {
      private readonly IHttpClient _httpClient;
      private readonly ITimingBlockFactory _timingBlockFactory;

      public MetricsHttpClient(
         IHttpClient httpClient,
         ITimingBlockFactory timingBlockFactory)
      {
         _httpClient = httpClient;
         _timingBlockFactory = timingBlockFactory;
      }

      public string Name => _httpClient.Name;

      public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         var timingBlock = _timingBlockFactory.Create("http_client");
         timingBlock.Dimensions.Add("type", "externally-decorated");

         var response = await timingBlock.ExecuteAsync(() => _httpClient.SendAsync(request, cancellationToken));
         return response;
      }
   }
}