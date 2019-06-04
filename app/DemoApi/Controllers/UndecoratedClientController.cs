using System.Threading.Tasks;
using DemoApi.Dependencies;
using DemoApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
   [Route("undecorated")]
   [ApiController]
   public class UndecoratedClientController : ControllerBase
   {
      private readonly DecoratedClient _client;
      private readonly EventSink _eventSink;

      public UndecoratedClientController(
         DecoratedClient client,
         EventSink eventSink)
      {
         _client = client;
         _eventSink = eventSink;
      }

      [HttpPost]
      public async Task Post()
      {
         await _eventSink.Post(_client, "undecorated");
      }
   }
}