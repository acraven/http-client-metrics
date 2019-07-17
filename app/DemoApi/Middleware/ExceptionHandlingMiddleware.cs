using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DemoApi.Extensions;

namespace DemoApi.Middleware
{
   // ReSharper disable once ClassNeverInstantiated.Global
   public class ExceptionHandlingMiddleware
   {
      private readonly RequestDelegate _next;
      private readonly ILogger<ExceptionHandlingMiddleware> _logger;

      public ExceptionHandlingMiddleware(
         RequestDelegate next,
         ILogger<ExceptionHandlingMiddleware> logger)
      {
         _next = next;
         _logger = logger;
      }

      public async Task Invoke(HttpContext context)
      {
         try
         {
            await _next(context);
         }
         catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
         {
            await context.Response.WriteAsync((HttpStatusCode)499, "Client Closed Request");
         }
         catch (Exception e)
         {
            _logger.LogError(e, "An unexpected exception has occurred.");

            await context.Response.WriteAsync(HttpStatusCode.InternalServerError, "Internal Server Error");
         }
      }
   }
}