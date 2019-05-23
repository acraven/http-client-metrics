//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Internal;
//
//namespace DemoApi
//{
//   public interface IHttpClient
//   {
//      Task PostAsync(string uri);
//   }
//
//   public class DefaultHttpClient : IHttpClient
//   {
//      private readonly HttpClient _httpClient;
//
//      public DefaultHttpClient(HttpClient httpClient)
//      {
//         _httpClient = httpClient;
//      }
//      
//      public async Task PostAsync(string uri)
//      {
//         await _httpClient.PostAsync(uri, new StringContent(string.Empty));
//      }
//   }
//}