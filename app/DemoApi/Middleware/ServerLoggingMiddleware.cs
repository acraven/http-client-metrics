using System;
using System.Net;
using System.Threading.Tasks;
using Grouchy.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DemoApi.Middleware
{
   public class ServerLoggingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly IMetricFactory _metricFactory;

      public ServerLoggingMiddleware(RequestDelegate next, IMetricFactory metricFactory)
      {
         _next = next;
         _metricFactory = metricFactory;
      }

      public Task Invoke(HttpContext context)
      {
         return Task.CompletedTask;

//         using (var metric = _metricFactory.CreateTimingMetric("http_request"))
//         {
//            try
//            {
//               await _next(context);
//            }
//            catch (Exception e)
//            {
//               await WriteResponse(context, HttpStatusCode.InternalServerError, "FAIL!");
//
//               metric.AddDimension("exception", e.GetType().Name);
//            }
//            
//            metric.AddDimension("statusCode", context.Response.StatusCode);
//         }
      }

      private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string plainText)
      {
         context.Response.StatusCode = (int)statusCode;
         context.Response.ContentType = "text/plain";
         await context.Response.WriteAsync(plainText);
      }
   }
}