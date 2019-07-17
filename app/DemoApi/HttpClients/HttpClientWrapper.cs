using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApi.HttpClients
{
   /// <summary>
   /// The HttpClientWrapper is fundamental to the externally decorated HttpClient pattern, but also supports the undecorated HttpClient pattern for the purpose
   /// of this spike
   /// </summary>
   public class HttpClientWrapper : IHttpClient
   {
      private readonly HttpClient _httpClient;

      public HttpClientWrapper(HttpClient httpClient)
      {
         _httpClient = httpClient;
      }

      public string Name { get; set; }

      public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      {
         var response = await _httpClient.SendAsync(request, cancellationToken);

         return response;
      }
   }
}