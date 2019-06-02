using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApi.Clients
{
   public class DelegatingHandlerClient : IHttpClientWrapper
   {
      private readonly HttpClient _httpClient;

      public DelegatingHandlerClient()
      {
         _httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler())) {BaseAddress = new Uri(Constants.ApiUri), Timeout = TimeSpan.FromSeconds(3)};
      }      
      
      public async Task PostAsync(string uri)
      {
         var response = await _httpClient.PostAsync(uri, new StringContent("{}"));

         response.EnsureSuccessStatusCode();
      }

      private class LoggingHandler : DelegatingHandler
      {
         public LoggingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
         {
         }
         
         protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
         {
//            Console.WriteLine("foo");

            await Task.Delay(1, cancellationToken);
            
            HttpResponseMessage response;

            try
            {
               response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
               Console.WriteLine(e);
               throw;
            }
            finally
            {
  //             Console.WriteLine("bar");
            }

            return response;
         }
      }
   }
}