using System.Threading.Tasks;
using DemoApi.Clients;
using Grouchy.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("decorated")]
    [ApiController]
    public class DecoratedClientController : ControllerBase
    {
        private readonly DecoratedClient _client;
        private readonly ITimingBlockFactory _timingBlockFactory;

        public DecoratedClientController(
            DecoratedClient client,
            ITimingBlockFactory timingBlockFactory)
        {
            _client = client;
            _timingBlockFactory = timingBlockFactory;
        }

        [HttpPost]
        public void Post()
        {
            var timingBlock = _timingBlockFactory.Create("http_client");
            
            // Fire-and-forget
            timingBlock.ExecuteAsync(async () => await _client.PostAsync("events/decorated"));
        }
    }
}
