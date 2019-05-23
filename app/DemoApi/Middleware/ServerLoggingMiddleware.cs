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
      private readonly ITimingBlockFactory _timingBlockFactory;

      public ServerLoggingMiddleware(RequestDelegate next, ITimingBlockFactory timingBlockFactory)
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
               await WriteResponse(context, HttpStatusCode.InternalServerError, "FAIL!");

               // TODO:
               // metric.AddDimension("exception", e.GetType().Name);
            }
            
            // TODO:
            // metric.AddDimension("statusCode", context.Response.StatusCode);
         });
      }

      private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string plainText)
      {
         context.Response.StatusCode = (int)statusCode;
         context.Response.ContentType = "text/plain";
         await context.Response.WriteAsync(plainText);
      }
   }
}