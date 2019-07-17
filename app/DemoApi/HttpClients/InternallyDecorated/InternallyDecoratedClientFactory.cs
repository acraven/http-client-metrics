using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoApi.Extensions;
using Grouchy.Abstractions;
using Grouchy.Polly.RateLimit;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace DemoApi.HttpClients.InternallyDecorated
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class InternallyDecoratedClientFactory : IHttpClientFactory
   {
      private readonly ITimingBlockFactory _timingBlockFactory;

      public InternallyDecoratedClientFactory(ITimingBlockFactory timingBlockFactory)
      {
         _timingBlockFactory = timingBlockFactory;
      }

      public IHttpClient Create()
      {
         var builder = new HttpMessageHandlerBuilder()
            .AddHandler(CreateTimeoutHandler())
            .AddHandler(new MetricsHandler(_timingBlockFactory))
            .AddHandler(CreateRateLimitHandler(new RateLimiter(10, TimeSpan.FromSeconds(1), 10, TimeSpan.FromSeconds(3))))
            .AddHandler(CreateRetryHandler(Constants.DefaultRetryDurationsMs));

         // TODO: Add concurrency limit

         var httpClient = new HttpClient(builder.Handler)
         {
            BaseAddress = new Uri(Constants.ApiUri)
         };

         return new HttpClientWrapper(httpClient) {Name = "internally-decorated"};
      }

      private DelegatingHandler CreateTimeoutHandler()
      {
         var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(Constants.DefaultHttpClientTimeout);

         return new PolicyHandler(timeoutPolicy);
      }

      private static PolicyHandler CreateRetryHandler(string retryDurationsMs)
      {
         var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .Or<RateLimitRejectionException>()
            .WaitAndRetryAsync(retryDurationsMs.ToDurations(), (a, b) => { Console.WriteLine("RETRY:...............................");});

         return new PolicyHandler(retryPolicy);
      }

      private static PolicyHandler CreateRateLimitHandler(IRateLimiter rateLimiter)
      {
         var rateLimitPolicy = new AsyncRateLimitPolicy<HttpResponseMessage>(rateLimiter, (context, exception) => Task.CompletedTask);

         return new PolicyHandler(rateLimitPolicy);
      }
   }
}