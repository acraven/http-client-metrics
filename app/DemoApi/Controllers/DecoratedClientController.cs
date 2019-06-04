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
      private readonly DecoratedClient _client;
      private readonly EventSink _eventSink;

      public DecoratedClientController(
         DecoratedClient client,
         EventSink eventSink)
      {
         _client = client;
         _eventSink = eventSink;
      }

      [HttpPost]
      public async Task Post()
      {
         await _eventSink.Post(_client, "decorated");
      }
   }
}