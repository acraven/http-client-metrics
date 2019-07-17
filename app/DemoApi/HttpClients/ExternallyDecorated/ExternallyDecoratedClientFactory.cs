using System;
using System.Net.Http;
using Grouchy.Abstractions;
using Grouchy.Polly.RateLimit;

namespace DemoApi.HttpClients.ExternallyDecorated
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class ExternallyDecoratedClientFactory : IHttpClientFactory
   {
      private readonly ITimingBlockFactory _timingBlockFactory;

      public ExternallyDecoratedClientFactory(ITimingBlockFactory timingBlockFactory)
      {
         _timingBlockFactory = timingBlockFactory;
      }

      public IHttpClient Create()
      {
         var baseHttpClient = new HttpClient
         {
            BaseAddress = new Uri(Constants.ApiUri),
            Timeout = Constants.DefaultHttpClientTimeout
         };

         IHttpClient httpClient = new HttpClientWrapper(baseHttpClient) {Name = "externally-decorated"};
         httpClient = new MetricsHttpClient(httpClient, _timingBlockFactory);
         httpClient = new RateLimitHttpClient(httpClient, new RateLimiter(10, TimeSpan.FromSeconds(1), 10, TimeSpan.FromSeconds(3)));
         httpClient = new RetryHttpClient(httpClient);

         // TODO: Add concurrencylimit

         return httpClient;
      }
   }
}