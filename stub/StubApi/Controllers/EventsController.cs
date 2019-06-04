using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StubApi.Controllers
{
   [Route("events")]
   [ApiController]
   public class EventsController : ControllerBase
   {
      [HttpPost("{name}")]
      public async Task Post([FromRoute] string name, CancellationToken cancellationToken)
      {
         await Task.Delay(50, cancellationToken);
      }
   }
}