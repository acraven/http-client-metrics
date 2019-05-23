using System;
using System.Threading.Tasks;
using DemoApi.Clients;
using Grouchy.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoApi.Controllers
{
    [Route("delegating-handler")]
    [ApiController]
    public class DelegatingHandlerClientController : ControllerBase
    {
        private readonly DelegatingHandlerClient _client;
        private readonly ITimingBlockFactory _timingBlockFactory;
        private readonly ILogger<DelegatingHandlerClientController> _logger;

        public DelegatingHandlerClientController(
            DelegatingHandlerClient client,
            ITimingBlockFactory timingBlockFactory,
            ILogger<DelegatingHandlerClientController> logger)
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
                await timingBlock.ExecuteAsync(async () => await _client.PostAsync("events/delegating-handler"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unexpected error has occurred");
            }
        }
    }
}
