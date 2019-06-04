using System.Net.Http;
using System.Threading.Tasks;

namespace DemoApi.HttpClients
{
   public interface IHttpClientWrapper
   {
      Task<HttpResponseMessage> PostAsync(string uri);
   }
}