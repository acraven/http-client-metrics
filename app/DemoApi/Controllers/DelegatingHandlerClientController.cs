using System.Threading.Tasks;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
   [Route("delegating-handler")]
   [ApiController]
   public class DelegatingHandlerClientController : ControllerBase
   {
      private readonly DecoratedClient _client;
      private readonly EventSink _eventSink;

      public DelegatingHandlerClientController(
         DecoratedClient client,
         EventSink eventSink)
      {
         _client = client;
         _eventSink = eventSink;
      }

      [HttpPost]
      public async Task Post()
      {
         await _eventSink.Post(_client, "delegating-handler");
      }
   }
}