using System.Threading.Tasks;

namespace DemoApi.Clients
{
   public interface IHttpClientWrapper
   {
      Task PostAsync(string uri);
   }
}