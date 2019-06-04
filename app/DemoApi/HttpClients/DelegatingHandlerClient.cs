using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grouchy.Abstractions;

namespace DemoApi.HttpClients
{
   // HttpClient will be decorated with metrics and resilience from the inside
   public class DelegatingHandlerClient : IHttpClientWrapper
   {
      private readonly HttpClient _httpClient;

      public DelegatingHandlerClient(ITimingBlockFactory timingBlockFactory)
      {
         _httpClient = new HttpClient(new MetricsHandler(new HttpClientHandler(), timingBlockFactory))
         {
            BaseAddress = new Uri(Constants.ApiUri),
            Timeout = TimeSpan.FromSeconds(3)
         };
      }

      public async Task<HttpResponseMessage> PostAsync(string uri)
      {
         var response = await _httpClient.PostAsync(uri, new StringContent("{}"));

         return response;
      }

      private class MetricsHandler : DelegatingHandler
      {
         private readonly ITimingBlockFactory _timingBlockFactory;

         public MetricsHandler(
            HttpMessageHandler innerHandler,
            ITimingBlockFactory timingBlockFactory) : base(innerHandler)
         {
            _timingBlockFactory = timingBlockFactory;
         }

         protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
         {
            var timingBlock = _timingBlockFactory.Create("http_client");

            var response = await timingBlock.ExecuteAsync(() => base.SendAsync(request, cancellationToken));

            return response;
         }
      }
   }
}