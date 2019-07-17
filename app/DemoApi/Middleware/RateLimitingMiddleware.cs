using System;
using System.Net;
using System.Threading.Tasks;
using Grouchy.Polly.RateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DemoApi.Extensions;

namespace DemoApi.Middleware
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class RateLimitingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<ExceptionHandlingMiddleware> _logger;
      private readonly AsyncRateLimitPolicy _rateLimitPolicy;

      public RateLimitingMiddleware(
         RequestDelegate next,
         ILogger<ExceptionHandlingMiddleware> logger)
      {
         _next = next;
         _logger = logger;

         var rateLimiter = new RateLimiter(20, TimeSpan.FromSeconds(1), 10, TimeSpan.FromSeconds(3));
         _rateLimitPolicy = new AsyncRateLimitPolicy(rateLimiter, (context, exception) => Task.CompletedTask);
      }

      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _rateLimitPolicy.ExecuteAsync(() => _next(context));
         }
         catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
         {
            await context.Response.WriteAsync((HttpStatusCode)429, "Too Many Requests");
         }
      }
   }
}