using DemoApi.Clients;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.Controllers
{
    [Route("delegating-handler")]
    [ApiController]
    public class DelegatingHandlerClientController : ControllerBase
    {
        private readonly DelegatingHandlerClient _client;

        public DelegatingHandlerClientController(DelegatingHandlerClient client)
        {
            _client = client;
        }

        [HttpPost]
        public void Post()
        {
            // Fire-and-forget
            MetricFactory.MeasureAsync(() => _client.PostAsync("events/delegating-handler"));
        }
    }
}
