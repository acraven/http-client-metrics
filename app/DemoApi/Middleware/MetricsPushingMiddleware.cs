using System;
using System.Threading.Tasks;
using Grouchy.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DemoApi.Middleware
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class MetricsPushingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ITimingBlockFactory _timingBlockFactory;

      public MetricsPushingMiddleware(RequestDelegate next, ITimingBlockFactory timingBlockFactory)
      {
         _next = next;
         _timingBlockFactory = timingBlockFactory;
      }

      public async Task Invoke(HttpContext context)
      {
         var timingBlock = _timingBlockFactory.Create("http_request");

         await timingBlock.ExecuteAsync(async () =>
         {
            try
            {
               await _next(context);
            }
            catch (Exception e)
            {
               // TODO: should be method on contract for exceptions
               timingBlock.Dimensions.Add("exception", e.GetType().Name);
            }

            timingBlock.Dimensions.Add("statusCode", context.Response.StatusCode);
         });
      }
   }
}