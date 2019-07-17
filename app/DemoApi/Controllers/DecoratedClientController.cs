using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
   [Route("decorated")]
   [ApiController]
   public class DecoratedClientController : ControllerBase
   {
      private readonly IHttpClient _httpClient;
      private readonly EventSink _eventSink;

      public DecoratedClientController(
         IEnumerable<IHttpClient> httpClients,
         EventSink eventSink)
      {
         _httpClient = httpClients.Single(c => c.Name == "externally-decorated");
         _eventSink = eventSink;
      }

      [HttpPost]
      public async Task Post()
      {
         await _eventSink.Post(_httpClient, "decorated");
      }
   }
}