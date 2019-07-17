using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StubApi.Middleware
{
   public class ServerLoggingMiddleware
   {
      private readonly RequestDelegate _next;

      public ServerLoggingMiddleware(RequestDelegate next)
      {
         _next = next;
      }

      public async Task Invoke(HttpContext context)
      {
         var stopwatch = Stopwatch.StartNew();

         try
         {
//TODO:            await Task.Delay(4000, context.RequestAborted);
            await _next(context);
         }
         catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
         {
            await WriteResponse(context, (HttpStatusCode)499, "ABORTED!");
         }
         catch (Exception e)
         {
            await WriteResponse(context, HttpStatusCode.InternalServerError, "FAIL!");

            Console.WriteLine($"{context.Response.StatusCode} {context.Request.Path} {stopwatch.ElapsedMilliseconds}ms {e.GetType().Name} {e.Message}");
         }

         Console.WriteLine($"{context.Response.StatusCode} {context.Request.Path} {stopwatch.ElapsedMilliseconds}ms");
      }

      private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, string plainText)
      {
         context.Response.StatusCode = (int) statusCode;
         context.Response.ContentType = "text/plain";
         await context.Response.WriteAsync(plainText);
      }
   }
}