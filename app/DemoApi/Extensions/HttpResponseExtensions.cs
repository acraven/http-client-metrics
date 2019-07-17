using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DemoApi.Extensions
{
   public static class HttpResponseExtensions
   {
      public static async Task WriteAsync(this HttpResponse response, HttpStatusCode statusCode, string plainText)
      {
         response.StatusCode = (int) statusCode;
         response.ContentType = "text/plain";
         await response.WriteAsync(plainText);
      }
   }
}