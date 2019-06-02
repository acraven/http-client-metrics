using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApi.Clients
{
   public class DecoratedClient : IHttpClientWrapper
   {
      private readonly HttpClient _httpClient;

      public DecoratedClient()
      {
         _httpClient = new HttpClient {BaseAddress = new Uri(Constants.ApiUri), Timeout = TimeSpan.FromSeconds(3)};
      }

      public async Task PostAsync(string uri)
      {
         var response = await _httpClient.PostAsync(uri, new StringContent("{}"));
         
         response.EnsureSuccessStatusCode();
      }
   }
}