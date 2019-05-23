using System;
using System.Threading.Tasks;
using DemoApi.Clients;
using Grouchy.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApi.Controllers
{
    [Route("undecorated")]
    [ApiController]
    public class UndecoratedClientController : ControllerBase
    {
        private readonly UndecoratedClient _client;
        private readonly ITimingBlockFactory _timingBlockFactory;
        private readonly ILogger<UndecoratedClientController> _logger;

        public UndecoratedClientController(
            UndecoratedClient client,
            ITimingBlockFactory timingBlockFactory,
            ILogger<UndecoratedClientController> logger)
        {
            _client = client;
            _timingBlockFactory = timingBlockFactory;
            _logger = logger;
        }

        [HttpPost]
        public void Post()
        {
            // Fire-and-forget
            PostEvent();
        }

        private async Task PostEvent()
        {
            var timingBlock = _timingBlockFactory.Create("http_client");

            try
            {
                await timingBlock.ExecuteAsync(() => _client.PostAsync("events/undecorated"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unexpected error has occurred");
            }
        }
    }
}
