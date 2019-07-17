using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
   [Route("delegating-handler")]
   [ApiController]
   public class DelegatingHandlerClientController : ControllerBase
   {
      private readonly IHttpClient _httpClient;
      private readonly EventSink _eventSink;

      public DelegatingHandlerClientController(
         IEnumerable<IHttpClient> httpClients,
         EventSink eventSink)
      {
         _httpClient = httpClients.Single(c => c.Name == "internally-decorated");
         _eventSink = eventSink;
      }

      [HttpPost]
      public async Task Post()
      {
         await _eventSink.Post(_httpClient, "delegating-handler");
      }
   }
}