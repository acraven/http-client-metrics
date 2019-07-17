using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoApi.HttpClients
{
   /// <summary>
   /// The IHttpClient is fundamental to the externally decorated HttpClient pattern, but also supports the undecorated HttpClient pattern for the purpose
   /// of this spike
   /// </summary>
   public interface IHttpClient
   {
      /// <summary>
      /// Contrived for the purposes of this spike
      /// </summary>
      string Name { get; }

      Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
   }
}